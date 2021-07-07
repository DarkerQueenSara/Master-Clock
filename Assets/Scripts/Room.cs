using Extensions;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject virtualCam;

    public LayerMask enemyLayer;

    //private Collider2D _confiner;
    [SerializeField] private List<GameObject> _enemies;
    private List<GameObject> _playerColliders;
    private bool _inRoom;

    public void Start()
    {
        _playerColliders = PlayerEntity.instance.colliders;
        _enemies = new List<GameObject>();
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (_playerColliders.Contains(other.gameObject) && !other.isTrigger)
        {
            _inRoom = true;
            //nao meter isto numa variavel, quebra o jogo
            virtualCam.GetComponent<CinemachineConfiner>().m_BoundingShape2D = GetComponent<PolygonCollider2D>();
            foreach (var e in _enemies)
            {
                e.SetActive(true);
            }
        }
        
        if (!_enemies.Contains(other.gameObject) && enemyLayer.HasLayer(other.gameObject.layer))
        {
            _enemies.Add(other.gameObject);
            if (!_inRoom) other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_playerColliders.Contains(other.gameObject) && !other.isTrigger)
        {
            _inRoom = false;
            foreach (var e in _enemies)
            {
                e.SetActive(false);
            }
        }
    }
}