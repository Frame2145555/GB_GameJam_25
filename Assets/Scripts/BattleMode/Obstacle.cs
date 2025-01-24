using UnityEngine;

public class Obstacle : MonoBehaviour
{
    float lifeTime = 10;

    [Header("References")]
    [SerializeField] GameObject visual;

    [Header("Vectors")]
    [SerializeField] Vector2 velocity;
    [SerializeField] Vector2 acceleration;
    [SerializeField] bool LockVectorToVelocity;

    

    float velocityAccelerationAngle;
    protected virtual void Start()
    {
        velocityAccelerationAngle = Vector2.Angle(velocity, acceleration);
        Destroy(gameObject,lifeTime);
    }

    void FixedUpdate()
    {
        Move();
        LookAtVelocity();
    }

    private void LookAtVelocity()
    {
        Vector2 direction = velocity.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        visual.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    protected virtual void Move()
    {
        transform.Translate(velocity * Time.deltaTime);
        velocity += acceleration * Time.deltaTime;
        if (LockVectorToVelocity)
        {
            acceleration = Quaternion.AngleAxis(velocityAccelerationAngle, Vector3.forward) * velocity.normalized * acceleration.magnitude;
        }
        else
        {
            velocityAccelerationAngle = Vector2.SignedAngle(velocity, acceleration);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + velocity);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + acceleration);
    }
}