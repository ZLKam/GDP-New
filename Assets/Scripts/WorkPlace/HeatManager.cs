using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeatChecks
{
    public class HeatManager : MonoBehaviour
    {
        public static bool windowOpen;
        public static bool fanOpen;
        public static bool airconOpen;

        [SerializeField] ApplianceAttribute window;
        [SerializeField] ApplianceAttribute fan;
        [SerializeField] ApplianceAttribute aircon;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var startLinePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (window != null && window.IsCollidingWith(startLinePos.x, startLinePos.y))
                {
                    windowOpen = true;
                }
                if (fan != null && fan.IsCollidingWith(startLinePos.x, startLinePos.y))
                {
                    fanOpen = true;
                }
                if (aircon != null && aircon.IsCollidingWith(startLinePos.x, startLinePos.y))
                {
                    airconOpen = true;
                }
            }
        }
    }
}
