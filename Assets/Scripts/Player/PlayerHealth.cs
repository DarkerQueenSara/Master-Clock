using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;
using UnityEngine.UI;
using Chronos;

public class PlayerHealth : MonoBehaviour
{
    public bool isAlive
    {
        get => currentHealth > 0;
    }

    public virtual bool IsAlive
    {
        get => currentHealth > 0;
    }

    public bool timeRunning = true;

    public LayerMask damagers;

    public float maxHealth;
    //[HideInInspector]
    public float currentHealth;

    //in seconds
    public float maxTime;
    [HideInInspector] public float currentTime;

    // UI
    [SerializeField] private Slider lifeBar;

    [SerializeField] private Slider timerBar;
    [SerializeField] private Text timeText;

    // Chronos
    [HideInInspector] public Clock clock;
    [HideInInspector] public Clock playerClock;

    private bool rewinding;

    // Start is called before the first frame update
    void Start()
    {
        // Get the global clock
        clock = Timekeeper.instance.Clock("Global");
        playerClock = Timekeeper.instance.Clock("Player");

        ResetCycle();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRunning) // Update Timer
        {
            currentTime -= clock.deltaTime;

            // UI
            this.timerBar.value = currentTime;

            float minutes = Mathf.FloorToInt(currentTime / 60);
            float seconds = Mathf.FloorToInt(currentTime % 60);

            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        if (currentTime <= 0 || currentHealth <= 0 || rewinding)
        {
            //Debug.Log(clock.time);
            if (clock.time > 0)
            {
                if (!rewinding)
                {
                    playerClock.localTimeScale = 1.0f; // In case player was rewinding or something reset their local time
                    GameObject cloneInstance = GameObject.FindGameObjectWithTag("Clone");
                    Destroy(cloneInstance, 0.0f);

                    clock.localTimeScale = 0f;
                    rewinding = true;
                }
            }
            else
            {
                rewinding = false;
            }

            ResetCycle();
        }
    }

    private void ResetCycle()
    {
        if (rewinding)
        {
            clock.localTimeScale = Mathf.Max(-3f, clock.localTimeScale - 0.01f);
        }
        else
        {
            clock.localTimeScale = 1f;

            Debug.Log("Cycle reset");
            currentHealth = maxHealth;
            currentTime = maxTime;

            // UI
            this.lifeBar.maxValue = maxHealth;
            this.lifeBar.value = maxHealth;

            this.timerBar.maxValue = maxTime;
            this.timerBar.value = maxTime;
        }

    }

    public void CollisionDetected(GameObject collider)
    {
        if (damagers.HasLayer(collider.layer))
        {
            if (collider.layer == 12)
            { // For the damage platforms hurt only if not in speed up mode
                if (playerClock.localTimeScale == 1.0f && clock.localTimeScale == 1.0f) // If player not rewinding and not speeding up
                {
                    Hit(1.0f);
                }
            }
            else
            {
                Hit(10.0f);
            }
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            // For the damage platforms hurt only if not in speed up mode
            if (playerClock.localTimeScale == 1.0f && clock.localTimeScale == 1.0f) // If player not rewinding and not speeding up
            {
                Hit(1.0f);
            }
        }
    }

    private void Hit(float damage)
    {
        if (!IsAlive || clock.localTimeScale <= 0 || playerClock.localTimeScale <= 0) return;
        currentHealth = Mathf.Max(currentHealth - damage, 0.0f);

        // UI
        this.lifeBar.value = currentHealth;

        //meter aqui que o player tem stun e inviciblity frames depois de um hit
        //if (!IsAlive) ResetCycle();
    }
}