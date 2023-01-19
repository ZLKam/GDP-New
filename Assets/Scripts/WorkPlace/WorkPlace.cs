using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using taskProgress;

public class WorkPlace : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Entrance;
    // Start is called before the first frame update
    void Start()
    {
        Player.transform.position = Entrance.transform.position;
        taskProgress.TaskProgress.heatCurrent += 20f;
        taskProgress.TaskProgress.CO2Current += 20f;
        Debug.Log(taskProgress.TaskProgress.heatCurrent);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
