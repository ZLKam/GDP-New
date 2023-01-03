using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractController : MonoBehaviour
{
    RandomEvent randomEvent;

    [Header("Player")]
    public GameObject player;

    [Header("Interact")]
    public Button interactButton;

    // Start is called before the first frame update
    void Start()
    {
        randomEvent = player.GetComponent<RandomEvent>();
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
        interactButton.gameObject.SetActive(true);

        randomEvent.rand = Random.Range(0, 2);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactButton.gameObject.SetActive(false);
    }
    
    private void InteractAction()
    {
        Debug.Log("button pressed");
    }
}
