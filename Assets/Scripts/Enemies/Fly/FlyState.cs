using UnityEngine;

public class FlyState : EnemyState<Fly>
{

    protected static new T Create<T>(Fly target) where T : FlyState
    {
        var state = EnemyState<Fly>.Create<T>(target);
        return state;
    }
    
    protected void Flip()
    {
        // Switch the way the player is labelled as facing.
        target.facingRight = !target.facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    
}