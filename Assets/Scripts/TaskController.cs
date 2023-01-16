using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskController : MonoBehaviour
{
    [SerializeField] private List<string> tasks = new();

    public Transform player;
    public TextMeshProUGUI taskHintText;
    public Button interactTaskButton;

    private Transform currentTask;
    private Collider2D currentTaskCollider;
    private GameObject taskArrow;

    private bool enteredTaskBefore = false;
    private bool showedCompletedText = false;
    private bool taskFinished = false;

    RandomEvent randomEvent;

    // Start is called before the first frame update
    void Start()
    {
        randomEvent = GetComponent<RandomEvent>();
        foreach (Transform child in transform)
        {
            if (child.name == "Task Arrow")
                continue;
            tasks.Add(child.name);
        }
        taskArrow = transform.Find("Task Arrow").GetComponent<SpriteRenderer>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (tasks.Count > 0)
        {
            currentTask = transform.Find(tasks[0]);
            currentTaskCollider = currentTask.gameObject.GetComponent<Collider2D>();
            DrawTaskHint(player.position, currentTask.position);
            taskHintText.text = "Next Task:" + "\n" + currentTask.name;

            CheckCollision();
            CheckPlayerInTask();
        }
        else
        {
            if (!showedCompletedText)
            {
                StartCoroutine(TaskHintTextCoroutine());
            }
            interactTaskButton.gameObject.SetActive(false);
            Debug.Log("all tasks completed");
        }

        SpriteRenderer spriteRenderer = currentTask.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
    }

    private void DrawTaskHint(Vector3 playerPosition, Vector3 taskPosition)
    {
        taskArrow.GetComponent<SpriteRenderer>().enabled = true;
        Vector3 offset = new(0, 1);
        Vector3 directionToTask = taskPosition - playerPosition;

        if (directionToTask.magnitude < 3f)
        {
            taskArrow.GetComponent<SpriteRenderer>().enabled = false;
            return;
        }
        directionToTask.Normalize();
        directionToTask *= 2f;

        Vector3 endPoint = playerPosition + directionToTask;
        taskArrow.transform.position = endPoint + offset;

        endPoint = transform.TransformPoint(endPoint);
        Vector3 rightAnglePoint = new(endPoint.x, playerPosition.y);
        float oppositeLength = Mathf.Abs((endPoint - rightAnglePoint).magnitude);
        float adjacentLength = Mathf.Abs((rightAnglePoint - playerPosition).magnitude);

        float theta = Mathf.Atan(oppositeLength / adjacentLength) * 180 / Mathf.PI;
        float xDirection = rightAnglePoint.x - playerPosition.x;
        float yDirection = endPoint.y - rightAnglePoint.y;
        if (xDirection > 0 && yDirection > 0)
        {
            taskArrow.transform.rotation = Quaternion.Euler(0, 0, theta);
        }
        else if (xDirection < 0 && yDirection > 0)
        {
            taskArrow.transform.rotation = Quaternion.Euler(0, 0, 180 - theta);
        }
        else if (xDirection < 0 && yDirection < 0)
        {
            taskArrow.transform.rotation = Quaternion.Euler(0, 0, 180 + theta);
        }
        else if (xDirection > 0 && yDirection < 0)
        {
            taskArrow.transform.rotation = Quaternion.Euler(0, 0, -theta);
        }
        
    }

    private void CheckPlayerInTask()
    {
        if (taskFinished == true)
        {
            transform.Find(tasks[0]).gameObject.SetActive(false);
            tasks.Remove(tasks[0]);
            if (tasks.Count > 0)
                transform.Find(tasks[0]).gameObject.SetActive(true);
            else
                return;
            taskFinished = false;
            enteredTaskBefore = false;
        }
    }

    IEnumerator TaskHintTextCoroutine()
    {
        taskHintText.text = "You had completed all tasks!";
        showedCompletedText = true;
        yield return new WaitForSeconds(5f);
        taskHintText.text = null;
    }

    private void CheckCollision()
    {
        if (currentTaskCollider.IsTouching(player.GetComponent<Collider2D>()))
        {
            interactTaskButton.gameObject.SetActive(true);
            interactTaskButton.onClick.RemoveAllListeners();
            interactTaskButton.onClick.AddListener(DoTask);
            
            if (!enteredTaskBefore)
            {
                randomEvent.rand = Random.Range(0f, 1f);
                randomEvent.DisplayEventTextFunction();
                enteredTaskBefore = true;
            }
        }
        else
        {
            if (ChangeSceneController.touchingDoor)
                return;
            interactTaskButton.gameObject.SetActive(false);
        }
    }

    private void DoTask()
    {
        taskFinished = true;
        interactTaskButton.gameObject.SetActive(false);
    }
}
