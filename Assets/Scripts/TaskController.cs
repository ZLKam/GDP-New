using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskController : MonoBehaviour
{
    [SerializeField] private List<string> tasks = new();

    public Transform player;
    public Material hintMaterial;
    public TextMeshProUGUI taskHintText;
    public Button interactTaskButton;

    private Transform currentTask;
    private Collider2D currentTaskCollider;

    private bool inTask = false;
    private bool canBlink = true;
    private bool showedCompletedText = false;
    private bool taskFinished = false;

    RandomEvent randomEvent;

    // Start is called before the first frame update
    void Start()
    {
        randomEvent = GetComponent<RandomEvent>();
        foreach (Transform child in transform)
        {
            tasks.Add(child.name);
            tasks.Sort();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tasks.Count > 0)
        {
            currentTask = transform.Find(tasks[0]);
            currentTaskCollider = currentTask.gameObject.GetComponent<Collider2D>();
            DrawTaskHint(player.position, currentTask.position, Color.yellow);
            taskHintText.text = "Next Task:" + "\n" + currentTask.name;

            CheckPlayerInTask();
        }
        else
        {
            if (!showedCompletedText)
            {
                StartCoroutine(TaskHintTextCoroutine());
            }
            Debug.Log("all tasks completed");
        }

        if (canBlink)
        {
            StartCoroutine(HintBlinkEffect());
        }
    }

    private void DrawTaskHint(Vector3 playerPosition, Vector3 taskPosition, Color color)
    {
        GameObject line = new();
        playerPosition.z = -5f;
        taskPosition.z = -5f;
        line.transform.position = playerPosition;
        line.AddComponent<LineRenderer>();
        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.material = hintMaterial;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.SetPosition(0, playerPosition);
        lineRenderer.SetPosition(1, taskPosition);
        Destroy(line, 2 * Time.deltaTime);
    }

    private void CheckPlayerInTask()
    {
        if (!inTask)
        {
            if (currentTaskCollider.IsTouching(player.GetComponent<Collider2D>()))
            {
                inTask = true;
                interactTaskButton.gameObject.SetActive(true);
                interactTaskButton.onClick.RemoveAllListeners();
                interactTaskButton.onClick.AddListener(DoTask);
                randomEvent.rand = Random.Range(0f, 1f);
                if (randomEvent.rand <= 0.5)
                    randomEvent.DisplayEventTextFunction();
            }
            else
            {
                return;
            }
        }
        if (taskFinished == true)
        {
            Debug.Log(currentTask.name);
            transform.Find(tasks[0]).gameObject.SetActive(false);
            tasks.Remove(tasks[0]);
            if (tasks.Count > 0)
                transform.Find(tasks[0]).gameObject.SetActive(true);
            else
                return;
            inTask = false;
            taskFinished = false;
        }
    }

    IEnumerator HintBlinkEffect()
    {
        SpriteRenderer spriteRenderer = currentTask.GetComponent<SpriteRenderer>();
        canBlink = false;
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(1f);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(2f);
        canBlink = true;
    }

    IEnumerator TaskHintTextCoroutine()
    {
        taskHintText.text = "You had completed all tasks!";
        showedCompletedText = true;
        yield return new WaitForSeconds(3f);
        taskHintText.text = null;
    }

    private void DoTask()
    {
        taskFinished = true;
        interactTaskButton.gameObject.SetActive(false);
    }
}
