using System.Collections.Generic;
using UnityEngine;

public class NonStaticPlatform : MonoBehaviour
{
    protected GameObject player;
    protected List<GameObject> playerColliders;
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        player = PlayerEntity.instance.gameObject;
        playerColliders = PlayerEntity.instance.colliders;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (playerColliders.Contains(other.gameObject))
        {
            player.transform.SetParent(gameObject.transform,true);
        }   
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (playerColliders.Contains(other.gameObject))
        {
            player.transform.parent = null;
        } 
    }
    
}
