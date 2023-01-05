using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    [SerializeField] private List<string> tasks = new();

    public Transform player;
    public Material hintMaterial;

    private Transform currentTask;
    private Collider2D currentTaskCollider;

    private bool canBlink = true;

    // Start is called before the first frame update
    void Start()
    {
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
        }
        else
        {
            Debug.Log("all tasks completed");
        }

        if (canBlink)
        {
            StartCoroutine(HintBlinkEffect());
        }
    }

    private void FixedUpdate()
    {
        if (currentTask != null)
        {
            CheckPlayerInTask();
        }
    }

    private void DrawTaskHint(Vector2 playerPosition, Vector2 taskPosition, Color color)
    {
        GameObject line = new();
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
        if (currentTaskCollider.IsTouching(player.GetComponent<Collider2D>()))
        {
            transform.Find(tasks[0]).gameObject.SetActive(false);
            tasks.Remove(tasks[0]);
            if (tasks.Count > 0)
                transform.Find(tasks[0]).gameObject.SetActive(true);
            else
                return;
        }
        else
        {
            return;
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
}
