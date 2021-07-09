using System;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public float swingSpeed;
    public float maxRotation;
    
    public float angleDelta;

    public bool startingLeft;

    private bool _swingingRight;

    public Transform ball;

    private bool _started;
    public void Start()
    {
        transform.rotation =
            startingLeft ? Quaternion.Euler(0, 0, -maxRotation + 1) : Quaternion.Euler(0, 0, maxRotation - 1);
        ball.localRotation = startingLeft ? Quaternion.Euler(0, 0, maxRotation) : Quaternion.Euler(0, 0, -maxRotation);
        _swingingRight = startingLeft;
        _started = true;
    }

    public void Update()
    {
        angleDelta = Mathf.Abs(Mathf.DeltaAngle(transform.rotation.eulerAngles.z, 0));
        if (angleDelta >= maxRotation)
        {
            _swingingRight = ball.position.x < transform.position.x;
        }

        float angleDiff = swingSpeed * Time.deltaTime;
        if (!_swingingRight) angleDiff *= -1;
        transform.Rotate(new Vector3(0, 0, angleDiff));
        ball.Rotate(new Vector3(0, 0, -angleDiff));
    }

    private void OnEnable()
    {
        if (_started) Start();
    }
}