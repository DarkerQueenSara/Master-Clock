using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;
using UnityEngine.UI;
using Chronos;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

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

    public float invincibilityTime = 1.2f;
    public float flashSpeed = 0.2f;

    private bool _isInvulnerable, _isInDamagingTiles;

    //in seconds
    public float maxTime;
    [HideInInspector] public float currentTime;

    // UI
    [SerializeField] public Slider lifeBar;

    [SerializeField] private Slider timerBar;
    [SerializeField] private Text timeText;

    // Chronos
    [HideInInspector] public Clock clock;
    [HideInInspector] public Clock playerClock;

    private bool rewinding;

    private PlayerMovement _playerMovement;
    private AudioManager _audioManager;
    private Animator _animator;

    private Volume rewindVolume;

    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Get the global clock
        clock = Timekeeper.instance.Clock("Global");
        playerClock = Timekeeper.instance.Clock("Player");
        _animator = transform.GetChild(0).GetComponent<Animator>();
        _playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        _audioManager = GetComponent<AudioManager>();
        GameObject rewindVolumeObj = GameObject.FindGameObjectWithTag("RewindVolume");
        if (rewindVolumeObj != null)
        {
            rewindVolume = rewindVolumeObj.GetComponent<Volume>();
        }

        initialPosition = gameObject.transform.position;

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
                    playerClock.localTimeScale =
                        1.0f; // In case player was rewinding or something reset their local time
                    GameObject
                        cloneInstance =
                            GameObject.FindGameObjectWithTag("Clone"); // Destroy all clones that might be in the scene
                    Destroy(cloneInstance, 0.0f);

                    _playerMovement.moveBlocked = true;
                    _animator.SetBool("MovementBlocked", true);
                    _playerMovement.StopPlayer();

                    clock.localTimeScale = 0f;
                    rewinding = true;

                    if (rewindVolume != null)
                    {
                        rewindVolume.enabled = true;
                    }

                    //gameObject.GetComponentInChildren<Collider2D>().enabled = false;
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
            clock.localTimeScale = Mathf.Max(-30f, clock.localTimeScale - 0.025f);
            //clock.localTimeScale = -30.0f;
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

            _playerMovement.moveBlocked = false;

            // Re-enable all dead enemies
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(true);
            }

            LevelManager.Instance.loops++;

            if (rewindVolume != null)
            {
                rewindVolume.enabled = false;
            }

            gameObject.transform.position = initialPosition;

            //gameObject.GetComponentInChildren<Collider2D>().enabled = true;
        }
    }

    public void CollisionDetected(GameObject collider)
    {
        // On rewind ignore collisions
        if (clock.localTimeScale <= 0.0f || playerClock.localTimeScale <= 0.0f)
            return;

        if (damagers.HasLayer(collider.layer) && !_isInvulnerable)
        {
            if (collider.layer == 12)
            {
                // For the damage platforms hurt only if not in speed up mode
                if (playerClock.localTimeScale == 1.0f &&
                    clock.localTimeScale == 1.0f) // If player not rewinding and not speeding up
                {
                    Hit(1.0f);
                    _isInDamagingTiles = true;
                    StartCoroutine(nameof(Flash));
                }
            }
            else
            {
                //Hit(10.0f);
                _isInvulnerable = true;
                Hit(collider.gameObject.GetComponent<EnemyBase>().contactDamage);
                Invoke(nameof(TurnOffInvulnerability), invincibilityTime);
                StartCoroutine(nameof(Flash));
            }
        }
        else if (collider.layer == 13)
        {
            // Door collision
            if (playerClock.localTimeScale > 1.0f)
            {
                // If accelerating and colliding with door
                DoorControl doorControl = collider.GetComponent<DoorControl>();
                if (doorControl.accelerateUnlocks)
                    doorControl.UnlockDoor();
            }
        }
        else if (collider.layer == 14)
        {
            // Powerup collision
            PickupPowerup(collider.GetComponent<PowerupDrop>());
        }
    }

    public void TurnOffInvulnerability()
    {
        _isInvulnerable = false;
    }

    public void CollisionUndetected(GameObject collider)
    {
        if (damagers.HasLayer(collider.layer))
        {
            if (collider.layer == 12)
            {
                _isInDamagingTiles = false;
            }
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            // For the damage platforms hurt only if not in speed up mode
            if (playerClock.localTimeScale == 1.0f &&
                clock.localTimeScale == 1.0f) // If player not rewinding and not speeding up
            {
                Hit(1.0f);
                _isInDamagingTiles = true;
            }
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == 12)
        {
            _isInDamagingTiles = false;
        }
    }

    private void PickupPowerup(PowerupDrop drop)
    {
        if (!drop.enabled) return;

        // Heath
        if (drop.give_health)
        {
            float chanceOfMoreHealth = Random.Range(0, 1);
            float healthToGain = chanceOfMoreHealth > 0.6 ? drop.health_amount * 3 :
                chanceOfMoreHealth < 0.3 ? drop.health_amount : drop.health_amount * 2;
            currentHealth = Mathf.Min(currentHealth + healthToGain, maxHealth);
            this.lifeBar.value = currentHealth;
        }

        if (drop.increase_max_health)
        {
            RectTransform rt = lifeBar.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(maxHealth + 10, 25);

            maxHealth += drop.max_health_increase_amount;
        }

        // Time
        /*
        if (drop.give_time)
        {
            clock. += drop.time_amount;
        }
        */

        // Powerup
        PlayerControls player_controls = this.gameObject.GetComponent<PlayerControls>();
        if (drop.give_extended)
            player_controls.UnlockPowerup("extended_attack");
        if (drop.give_clone)
            player_controls.UnlockPowerup("clone_attack");
        if (drop.give_slowdown)
            player_controls.UnlockPowerup("slowdown_bomb_attack");
        if (drop.give_accelerate)
            player_controls.UnlockPowerup("accelerate_attack");
        if (drop.give_spin)
            player_controls.UnlockPowerup("spin_attack");
        if (drop.give_slide)
            player_controls.UnlockPowerup("slide");

        if (!drop.give_health)
        {
            LevelManager.Instance.collectedItems++;
            _audioManager.Play("CollectUpgrade");
        }
        else
        {
            _audioManager.Play("CollectHealth");

        }

        // Destroy drop
        Destroy(drop.gameObject, 0.0f);
    }

    public void Hit(float damage)
    {
        if (!IsAlive || clock.localTimeScale <= 0 || playerClock.localTimeScale <= 0) return;
        currentHealth = Mathf.Max(currentHealth - damage, 0.0f);
        
        _audioManager.Play("Hit");
        // UI
        this.lifeBar.value = currentHealth;

        //if (!IsAlive) ResetCycle();
    }

    public IEnumerator Flash()
    {
        SpriteRenderer r = GetComponentInChildren<SpriteRenderer>();
        while (_isInvulnerable || _isInDamagingTiles)
        {
            r.material.SetFloat("_FlashAmount", 0.8f);
            yield return new WaitForSeconds(flashSpeed);
            r.material.SetFloat("_FlashAmount", 0);
            yield return new WaitForSeconds(flashSpeed);
        }
    }
}