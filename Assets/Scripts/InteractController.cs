using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InteractController : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;

    [Header("Interact")]
    public Button interactButton;

    // Update is called once per frame
    void Update()
    {
        if (interactButton.gameObject.activeSelf == true)
        {
            interactButton.onClick.RemoveAllListeners();
            interactButton.onClick.AddListener(InteractAction);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.CompareTag("Entrance"))
        {
            interactButton.GetComponentInChildren<TextMeshProUGUI>().text = "Enter";
            interactButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactButton.gameObject.SetActive(false);
    }
    
    private void InteractAction()
    {
        SceneManager.LoadScene("Day1 Inside");
    }
}
