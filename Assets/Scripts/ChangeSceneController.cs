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

    private void Start()
    {
        currentScreen = SceneManager.GetActiveScene().name;
    }

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
}
