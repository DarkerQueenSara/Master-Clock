using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    private GameObject player;
    
    public bool unlocked;
    private bool opening = false;
    private bool closing = true;

    public float distToUnlock = 3.0f;

    public float timeToReachTarget = 0.1f;
    private float t;

    public float distanceTravelUp = 3.0f;

    private Vector3 originalPosition;

    [Header("Attack Unlocks")]
    public bool normalAttackUnlocks;
    public bool spinAttackUnlocks;
    public bool extendedAttackUnlocks;
    public bool cloneAttackUnlocks;
    public bool slowdownBombAttackUnlocks;
    public bool accelerateUnlocks;

    public void Start()
    {
        player = PlayerEntity.instance.gameObject;
        originalPosition = this.transform.position;

        if (unlocked)
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
       /* else
        {
            this.GetComponent<SpriteRenderer>().color = Color.red;
        }*/
    }

    public void Update()
    {
        if (Vector2.Distance(player.transform.position, originalPosition) <= distToUnlock)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    public void OpenDoor()
    {
        if (unlocked) {
            if (closing)
            {
                t = 0;
                closing = false;
            }

            opening = true;

            t += Time.deltaTime / timeToReachTarget;

            this.transform.position = Vector3.Lerp(this.originalPosition, this.originalPosition + Vector3.up * distanceTravelUp, t);
        }
    }

    public void CloseDoor()
    {
        if (unlocked)
        {
            if (opening)
            {
                t = 0;
                opening = false;
            }

            closing = true;

            t += Time.deltaTime / timeToReachTarget;

            this.transform.position = Vector3.Lerp(this.originalPosition + Vector3.up * distanceTravelUp, this.originalPosition, t);
        }
    }

    public void UnlockDoor()
    {
        /*
        switch (attackName)
        {
            case "NormalAttack":
                if (normalAttackUnlocks)
                    unlocked = true;
                break;

            case "ExtendedAttack":
                if (extendedAttackUnlocks)
                    unlocked = true;
                break;

            case "CloneAttack":
                if (cloneAttackUnlocks)
                    unlocked = true;
                break;

            case "SlowdownBombAttack":
                if (slowdownBombAttackUnlocks)
                    unlocked = true;
                break;
            default:
                break;
        }
        */

        if (!unlocked)
        {
            this.GetComponent<SpriteRenderer>().color = Color.blue;
            unlocked = true;
        }
    }

    public void LockDoor()
    {
        if (unlocked)
        {
            this.GetComponent<SpriteRenderer>().color = Color.red;
            unlocked = false;
        }
    }
}
