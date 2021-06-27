using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    //0 = slash
    public List<GameObject> hitboxes;

    public void SlashAttack()
    {
        //dar trigger da animação slash attack
    }
    
    public void ToggleHitbox(int index, bool state)
    {
        hitboxes[index].SetActive(state);
    }
}