using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayModeController : MonoBehaviour
{
    Rigidbody2D rb;
    Fade fade;

    [Header("Speed Control")]
    [SerializeField] float speed = 10;

    [SerializeField] List<celineanimation> anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fade = FindAnyObjectByType<Fade>();

        BattleModeManager.Instance.OnBattleModeEnter += OnBattleModeEnter;
    }

    private void OnDisable()
    {
        BattleModeManager.Instance.OnBattleModeEnter -= OnBattleModeEnter;
    }
    private void Update()
    {
        if (DialogueManager.Instance.DialogueIsPlaying || fade.IsFadingOut)
        {
            rb.linearVelocityX = 0;
            return;
        }

        float dir = InputManager.Instance.GetMoveVector().x;
        rb.linearVelocityX = dir * speed;

        for (int i = 0; i < anim.Count; i++)
        {
            if (InputManager.Instance.GetMoveVector().x != 0)
            {
                anim[i].runanmation();
            }
            else
            {
                anim[i].idleanmation();
            }
        }

    }
        void OnBattleModeEnter()
        {
            gameObject.SetActive(false);
        }
}
