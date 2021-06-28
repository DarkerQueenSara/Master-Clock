using Extensions;
using UnityEngine;

public class CrawlerCrawling : CrawlerState
{
    private bool _hitFloor = false;
    private float _colliderSize = 0.5f;
    private Vector2 _direction = new Vector2(1,0);

    public static CrawlerCrawling Create(Crawler target)
    {
        return CrawlerState.Create<CrawlerCrawling>(target);
    }

    public override void StateStart()
    {
        base.StateStart();
        _colliderSize = target.boxCollider.bounds.extents.y;
    }

   public override void StateUpdate()
    {
        if (!_hitFloor)
        {
            transform.localPosition += (Vector3)(_direction.normalized + Vector2.down) * target.moveSpeed * Time.deltaTime;
        }
        else
        {
            Vector3 moveVector;
            if (transform.parent != null)
            {
                moveVector = transform.parent.InverseTransformVector(target.head.right * target.moveSpeed * Time.deltaTime);
            }
            else
            {
                moveVector = target.head.right * target.moveSpeed * Time.deltaTime;
            }
            transform.localPosition += moveVector;
            bool horizontal = _direction.x != 0;

            RaycastHit2D down = Physics2D.Raycast(target.head.position, -target.head.up, target.rayDistance, target.groundMask);
            Debug.DrawRay(target.head.position, -target.head.up * target.rayDistance, Color.red);
            RaycastHit2D forward = Physics2D.Raycast(target.head.position, target.head.right, target.rayDistance, target.groundMask);
            Debug.DrawRay(target.head.position, target.head.right * target.rayDistance, Color.blue);
            //RaycastHit2D wallHit = Physics2D.Raycast(target.wallFinder.position, -target.wallFinder.right, target.rayDistance, target.groundMask);
            //Debug.DrawRay(target.wallFinder.position, -target.wallFinder.right * target.rayDistance, Color.black);
            
            //escolher direção do down
            if (down.collider == null && forward.collider == null)
            {
                RaycastHit2D wallHit = Physics2D.Raycast(target.wallFinder.position, -target.wallFinder.right, target.rayDistance, target.groundMask);
                Debug.DrawRay(target.wallFinder.position, -target.wallFinder.right * target.rayDistance, Color.green);
                if (wallHit.normal != Vector2.zero)
                {
                    transform.Rotate(Vector3.forward, -90 * Mathf.Sign(_direction.x));
                    transform.position = wallHit.point + wallHit.normal * _colliderSize;
                }
            }
            //escolher direção oposta ao down
            else if (down.collider != null && forward.collider != null)
            {
                transform.Rotate(Vector3.forward, 90 * Mathf.Sign(_direction.x));
                transform.position = forward.point + forward.normal * _colliderSize;
            }
        }
    }
   protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (!_hitFloor && target.groundMask.HasLayer(collider.gameObject.layer))
        {
            _hitFloor = true;
            Vector2 closestPoint = collider.ClosestPoint(target.rb.position);
            Vector2 rbPos = new Vector2(target.rb.position.x, target.rb.position.y);
            Vector2 normal = (rbPos - closestPoint).normalized;
            RaycastHit2D hit;
            bool wall = Vector2.Angle(normal, Vector2.up) > 80;

            //se for parede
            if (wall)
            {
                if (_direction.x > 0)
                {
                    transform.Rotate(Vector3.forward, 90);
                }
                else
                {
                    transform.Rotate(Vector3.forward, -90);
                }
            }
            _direction = new Vector2(Mathf.Sign(_direction.x), 0);
            hit = Physics2D.Raycast(target.head.position, -transform.up, target.rayDistance, target.groundMask);
            Vector3 newPos = hit.point + hit.normal * _colliderSize;
            transform.position = newPos;
        }
    }
}