using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupDrop : MonoBehaviour
{
    [Header("Health")]
    public bool give_health;
    public float health_amount;

    [Header("Time")]
    public bool give_time;
    public int time_amount;

    [Header("Powerups")]
    public bool give_extended;
    public bool give_clone;
    public bool give_slowdown;
    public bool give_spin;
    public bool give_accelerate;

    [Header("Spin&Animation")]
    public float timeToReachTarget = 1.1f;
    public float distanceUp = 0.3f;
    public float rotationPerFrame = 0.25f;
    private float t;
    private bool goingUp = true;

    private Vector3 originalPosition;

    public void Start()
    {
        originalPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0.0f, rotationPerFrame, 0.0f));

        t += Time.deltaTime / timeToReachTarget;

        if (goingUp)
            this.transform.position = Vector3.Lerp(this.originalPosition, this.originalPosition + Vector3.up * distanceUp, t);
        else
            this.transform.position = Vector3.Lerp(this.originalPosition + Vector3.up * distanceUp, this.originalPosition, t);

        if (t >= timeToReachTarget)
        {
            t = 0;
            goingUp = !goingUp;
        }
    }
}
