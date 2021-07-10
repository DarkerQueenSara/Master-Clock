using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float timeToDestroy;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Despawn), timeToDestroy);
    }

    // Update is called once per frame
    void Despawn()
    {
        Destroy(gameObject);
    }
}
