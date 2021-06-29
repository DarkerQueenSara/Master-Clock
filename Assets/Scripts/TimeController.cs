using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class TimeController : MonoBehaviour
{
    private Clock clock;

    private void Start()
    {
        // Get the global clock
        clock = Timekeeper.instance.Clock("Global");
    }

    void Update()
    {
        // Change its time scale on key press
        if (Input.GetKeyDown(KeyCode.R))
        {
            clock.localTimeScale = -1; // Rewind
        }
        //else if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    clock.localTimeScale = 0; // Pause
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    clock.localTimeScale = 0.5f; // Slow
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    clock.localTimeScale = 1; // Normal
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    clock.localTimeScale = 2; // Accelerate
        //}
    }
}
