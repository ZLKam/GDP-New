using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace taskProgress
{
    public class TaskProgress : MonoBehaviour
    {
        public static float taskMaximum = 100;
        public static float taskCurrent = 0;
        [SerializeField] private Image taskMask;

        public static float heatMaximum = 100;
        public static float heatCurrent = 0;
        [SerializeField] private Image heatMask;

        public static float CO2Maximum = 100;
        public static float CO2Current = 0;
        [SerializeField] private Image CO2Mask;

        public static float heatFillAmount;
        public static float CO2FillAmount;

        public static GameObject taskProgressBar;
        // Start is called before the first frame update
        void Start()
        {
            taskProgressBar = transform.Find("TaskProgress").gameObject;
            if (taskProgressBar.activeInHierarchy) 
            {
                taskProgressBar.SetActive(false);
            }
            
        }

        // Update is called once per frame
        void Update()
        {
            getCurrentFill();
        }

        void getCurrentFill()
        {
            taskMask.fillAmount = taskCurrent / taskMaximum;
            heatMask.fillAmount = heatCurrent/ heatMaximum;
            CO2Mask.fillAmount = CO2Current / CO2Maximum;

            heatFillAmount = heatMask.fillAmount;
            CO2FillAmount = CO2Mask.fillAmount;
        }
    }
}
