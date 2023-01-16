using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomEvent : MonoBehaviour
{
    public TextMeshProUGUI randomEventText;

    public float rand;

    IEnumerator DisplayEventText()
    {
        yield return new WaitForSeconds(2);
        rand = 0;
        randomEventText.gameObject.SetActive(false);
    }

    public void DisplayEventTextFunction()
    {
        if (rand <= 0.5f)
        {
            //randomEventText.text = "";
            randomEventText.gameObject.SetActive(true);
            StartCoroutine(DisplayEventText());
        }
    }
}
