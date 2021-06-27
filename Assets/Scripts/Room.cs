using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject virtualCam;
    //private Collider2D _confiner;
    public List<GameObject> enemies;

    private void Start()
    {
        //_confiner = virtualCam.GetComponent<CinemachineConfiner>().m_BoundingShape2D;
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            //nao meter isto numa variavel, quebra o jogo
            virtualCam.GetComponent<CinemachineConfiner>().m_BoundingShape2D = GetComponent<PolygonCollider2D>();
            foreach (var e in enemies)
            {
                e.SetActive(true);
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            foreach (var e in enemies)
            {
                e.SetActive(false);
            }
        }
    }
}
