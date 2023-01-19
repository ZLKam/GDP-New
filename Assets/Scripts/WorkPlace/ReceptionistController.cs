using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionistController : MonoBehaviour
{
    [SerializeField] private GameObject PolicyUI;

    private bool seenPolicy = false;
    // Start is called before the first frame update
    void Start()
    {
        PolicyUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !seenPolicy) 
        {
            PolicyUI.SetActive(true);
            Time.timeScale = 0f;
            seenPolicy = !seenPolicy;
        }
    }
    public void ClosePressed()
    {
        if (PolicyUI.activeInHierarchy)
        {
            PolicyUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
