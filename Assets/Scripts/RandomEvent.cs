using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomEvent : MonoBehaviour
{
    public TextMeshProUGUI randomEventText;

    public int rand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rand == 1)
        {
            randomEventText.gameObject.SetActive(true);
            StartCoroutine(DisplayEventText());
        }
        else
        {
            randomEventText.gameObject.SetActive(false);
        }
    }

    IEnumerator DisplayEventText()
    {
        yield return new WaitForSeconds(2);
        rand = 0;
        randomEventText.gameObject.SetActive(false);
    }
}
