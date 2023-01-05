using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterGame : MonoBehaviour
{
    Button enterGameButton;

    // Start is called before the first frame update
    void Start()
    {
        enterGameButton = gameObject.GetComponent<Button>();
        enterGameButton.onClick.AddListener(EnterGameFunction);
    }

    private void EnterGameFunction()
    {
        SceneManager.LoadScene("Day1 Outside");
    }
}
