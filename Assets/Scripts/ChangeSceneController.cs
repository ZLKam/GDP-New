using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ChangeSceneController : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;

    [Header("Interact")]
    public Button interactButton;
    
    string currentScreen;
    
    public static bool touchingDoor = false;

    private void Start()
    {
        currentScreen = SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.CompareTag("Entrance"))
        {
            touchingDoor = true;
            if (currentScreen == "Day1 Inside Level 1")
            {
                interactButton.GetComponentInChildren<TextMeshProUGUI>().text = "Exit";
                interactButton.gameObject.SetActive(true);
                interactButton.onClick.RemoveAllListeners();
                interactButton.onClick.AddListener(ExitScene);
                return;
            }
            interactButton.GetComponentInChildren<TextMeshProUGUI>().text = "Enter";
            interactButton.gameObject.SetActive(true);
            interactButton.onClick.RemoveAllListeners();
            interactButton.onClick.AddListener(InteractAction);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        touchingDoor = false;
        interactButton.gameObject.SetActive(false);
    }

    private void InteractAction()
    {
        switch (currentScreen)
        {
            case "Day1 Outside":
                SceneManager.LoadScene("Day1 Inside Level 1");
                break;
            case "Day1 Inside Level 1":
                SceneManager.LoadScene("Day1 Inside Level 2");
                break;
            case "Day1 Inside Level 2":
                break;
            default:
                Debug.Log("You faced an error!");
                break;
        }
    }

    private void ExitScene()
    {
        SceneManager.LoadScene("Day1 Outside");
    }
}
