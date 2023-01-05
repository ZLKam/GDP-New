using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomEvent : MonoBehaviour
{
    public TextMeshProUGUI randomEventText;

    public int rand;

    void Awake()
    {
        rand = Random.Range(0, 2);
    }

    IEnumerator DisplayEventText()
    {
        yield return new WaitForSeconds(2);
        rand = 0;
        randomEventText.gameObject.SetActive(false);
    }

    public void DisplayEventTextFunction()
    {
        randomEventText.gameObject.SetActive(true);
        StartCoroutine(DisplayEventText());
    }
}
