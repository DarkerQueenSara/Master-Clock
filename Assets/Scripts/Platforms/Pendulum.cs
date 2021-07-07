using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public float swingSpeed;
    public float maxRotation;

    public bool startingLeft;

    private bool _swingingRight;

    public Transform ball;

    public void Start()
    {
        transform.rotation =
            startingLeft ? Quaternion.Euler(0, 0, -maxRotation + 1) : Quaternion.Euler(0, 0, maxRotation - 1);
        ball.localRotation = startingLeft ? Quaternion.Euler(0, 0, maxRotation) : Quaternion.Euler(0, 0, -maxRotation);
        _swingingRight = startingLeft;
    }

    public void Update()
    {
        if (Mathf.Abs(Mathf.DeltaAngle(transform.rotation.eulerAngles.z, 0)) >= maxRotation)
        {
            _swingingRight = !_swingingRight;
        }

        float angleDiff = swingSpeed * Time.deltaTime;
        if (!_swingingRight) angleDiff *= -1;
        transform.Rotate(new Vector3(0, 0, angleDiff));
        ball.Rotate(new Vector3(0, 0, -angleDiff));
    }
}