using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

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

    public int maxHealth;
    //[HideInInspector]
    public int currentHealth;

    //in seconds
    public float maxTime;
    [HideInInspector] public float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentTime = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRunning) currentTime -= Time.deltaTime;
        if (currentTime <= 0 || currentHealth <= 0) ResetCycle();
    }

    private void ResetCycle()
    {
        Debug.Log("Cycle reset");
        currentHealth = maxHealth;
        currentTime = maxTime;
    }

    public void CollisionDetected(GameObject collider)
    {
        if (damagers.HasLayer(collider.layer))
        {
            Hit(1);
        }
    }

    private void Hit(int damage)
    {
        if (!IsAlive) return;
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        //meter aqui que o player tem stun e inviciblity frames depois de um hit
        if (!IsAlive) ResetCycle();
    }
}