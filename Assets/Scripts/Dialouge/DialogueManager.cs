using UnityEngine;
using TMPro;
using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;
using UnityEditor.Animations;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    static DialogueManager instance;

    [Header("Dialogue UI")]
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI speakerText;
    [SerializeField] GameObject characterDummy;

    [Header("Choice UI")]
    [SerializeField] GameObject[] choices;

    TextMeshProUGUI[] choicesText;

    //Character handle
    Dictionary<string, GameObject> characterStore = new Dictionary<string, GameObject>();
    Dictionary<string, AnimatorController> animControllerStore = new Dictionary<string, AnimatorController>();

    string target = "";
    RectTransform targetRectTransform;
    Animator targetAnimator;

    Story currentStory;
    
    bool dialogueIsPlaying;

    const string SPEAKER_TAG = "Speaker";
    const string SPAWN_TAG = "Spawn";
    const string TARGET_TAG = "Target";
    const string FACE_TAG = "Face";
    const string XPOS_TAG = "x";
    const string YPOS_TAG = "y";
    const string SIZE_TAG = "size";

    Color32 GRAYOUT_COLOR = new Color32(127, 127, 127, 255);
    
    public static DialogueManager Instance { get => instance; } 
    public bool DialogueIsPlaying { get => dialogueIsPlaying; }

    private void Awake()
    {
        if (instance != null && instance !=this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        StartCoroutine(ExitDialogueMode());

        // Get all of the choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int i = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[i] = choice.GetComponentInChildren<TextMeshProUGUI>();
            i++;
        }
    }

    private void Update()
    {
        //return if no dialogue is play
        if (!dialogueIsPlaying) return;

        //handle continuing to the next line
        if (InputManager.Instance.IsInteractKeyDown)
        {
            ContinueStory();
        }

    }

    public void EnterDialogueMode(TextAsset inkJSON, List<Pair<string, AnimatorController>> animControllers)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        foreach (var controller in animControllers)
        {
            animControllerStore.Add(controller.First,controller.Second);
        }

        ContinueStory();
    }

    IEnumerator ExitDialogueMode()
    {
        yield return new WaitForEndOfFrame();

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        target = "";
        targetAnimator = null;
        targetRectTransform = null;

        foreach (var character in characterStore)
        {
            Destroy(character.Value);
        }
        characterStore.Clear();
        animControllerStore.Clear();
    }

    void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();

            DisplayChoices();

            Parsetag(currentStory.currentTags);
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    void Parsetag(List<string> currentTags)
    {
        foreach(string tag in currentTags)
        {
            string key = tag.Split(':')[0].Trim();
            string value = tag.Split(':')[1].Trim();

            Debug.Log("Parsed Tag -> key : " + key + " | value : " + value);

            switch (key)
            {
                case SPEAKER_TAG:
                    speakerText.text = value;
                    break;
                case SPAWN_TAG:
                    characterStore.Add(value, Instantiate(characterDummy,dialoguePanel.transform.parent));
                    characterStore[value].SetActive(true);
                    characterStore[value].transform.SetAsFirstSibling();
                    characterStore[value].GetComponent<Animator>().runtimeAnimatorController 
                        = animControllerStore[value];
                    break;
                case TARGET_TAG:
                    foreach (var character in characterStore)
                    {
                        character.Value.GetComponent<Image>().color = GRAYOUT_COLOR;
                    }
                    target = value;
                    targetRectTransform = characterStore[value].GetComponent<RectTransform>();
                    targetAnimator = characterStore[value].GetComponent<Animator>();
                    characterStore[value].GetComponent<Image>().color = Color.white;
                    break;
                case FACE_TAG:
                    targetAnimator.Play(value);
                    break;
                case XPOS_TAG:
                    targetRectTransform.localPosition = new Vector3(int.Parse(value), targetRectTransform.localPosition.y);
                    break;
                case YPOS_TAG:
                    targetRectTransform.localPosition = new Vector3(targetRectTransform.localPosition.x, int.Parse(value));
                    break;
                case SIZE_TAG:
                    targetRectTransform.localScale = Vector3.one * float.Parse(value); 
                    break;
                default:
                    Debug.Log("Tag key doesn't exist. Tag key : " + key);
                    break;
            }
        }
    }

    void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if(currentChoices.Count > choices.Length)
        {
            Debug.LogWarning("More choices were given more than the UI can support. Number of choice given : " + currentChoices.Count);
        }

        int i = 0;
        foreach(Choice choice in currentChoices)
        {
            choices[i].gameObject.SetActive(true);
            choicesText[i].text = choice.text;
            i++;
        }

        for (int j = i; j < choices.Length; j++)
        {
            choices[j].gameObject.SetActive(false);
        }
        StartCoroutine(SelectFirstChoiche());
    }

    IEnumerator SelectFirstChoiche()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);

    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

}
