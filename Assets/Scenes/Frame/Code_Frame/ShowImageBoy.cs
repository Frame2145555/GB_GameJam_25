using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShowImageBoy : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private GameObject imageBoy;

    private bool isPlayerInside = false;

    private void Start()
    {
        if (imageBoy != null)
        {
            imageBoy.SetActive(false);
        }
        else
        {
            Debug.LogWarning("ImageBoy is not assigned in the inspector!");
        }
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.Space))
        {
            if (imageBoy != null)
            {
                imageBoy.SetActive(false);
            }

            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = true;

            if (imageBoy != null)
            {
                imageBoy.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = false;

            if (imageBoy != null)
            {
                imageBoy.SetActive(false);
            }
        }
    }
}
