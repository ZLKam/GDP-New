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

    private bool work = false;

    private void Start()
    {
        currentScreen = SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.CompareTag("Entrance"))
        {
            touchingDoor = true;
            if (currentScreen == "Workplace")
            {
                interactButton.GetComponentInChildren<TextMeshProUGUI>().text = "Exit";
            }
            else
                interactButton.GetComponentInChildren<TextMeshProUGUI>().text = "Enter";
            interactButton.gameObject.SetActive(true);
            interactButton.onClick.RemoveAllListeners();
            interactButton.onClick.AddListener(InteractAction);
        }
        else if (collision.CompareTag("Player") && gameObject.CompareTag("Work Entrance"))
        {
            work = true;
            interactButton.GetComponentInChildren<TextMeshProUGUI>().text = "Enter";
            interactButton.gameObject.SetActive(true);
            interactButton.onClick.RemoveAllListeners();
            interactButton.onClick.AddListener(EnterWork);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        touchingDoor = false;
        work = false;
        interactButton.gameObject.SetActive(false);
    }

    private void InteractAction()
    {
        switch (currentScreen)
        {
            case "Day1 Outside":
                SceneManager.LoadScene("Day1 Inside Level 1");
                break;
            case "Workplace":
                SceneManager.LoadScene("Day1 Outside");
                break;
            default:
                Debug.Log("You faced an error!");
                break;
        }
    }

    private void EnterWork() 
    {
        if (work) 
        {
            SceneManager.LoadScene("Workplace");
        }
    }
}
