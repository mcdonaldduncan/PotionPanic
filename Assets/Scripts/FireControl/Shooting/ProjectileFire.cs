using UnityEngine;
using UnityEngine.Pool;

public class ProjectileFire : MonoBehaviour, IPoolable<ProjectileFire>
{
    [SerializeField] float radius = 1.1f;
    [SerializeField] float initialAngle;
    [SerializeField] int damagePerShot = 1;
    [SerializeField] LayerMask enemy;

    Rigidbody _rb;

    Vector3 initialPos;

    public IObjectPool<ProjectileFire> Pool { get; set; }

    private void OnEnable()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody>();

        _rb.velocity = Vector3.zero;
    }

    private void OnDisable()
    {
        _rb.velocity = Vector3.zero;
    }

    public void ReturnToPool()
    {
        if (gameObject.activeSelf == true)
        {
            Pool.Release(this);
        }
    }

    public void Launch(Vector3 _target)
    {
        initialPos = transform.position;
        Vector3 targetPos = _target;

        float gravity = Physics.gravity.magnitude;
        float angle = initialAngle * Mathf.Deg2Rad;

        Vector3 planarTarget = new Vector3(targetPos.x, 0, targetPos.z);
        Vector3 planarPostion = new Vector3(transform.position.x, 0, transform.position.z);

        float distance = Vector3.Distance(planarTarget, planarPostion);
        float yOffset = transform.position.y - targetPos.y;

        // Original equation https://physics.stackexchange.com/questions/27992/solving-for-initial-velocity-required-to-launch-a-projectile-to-a-given-destinat?rq=1
        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPostion);
        if (targetPos.x < transform.position.x) angleBetweenObjects *= -1;
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        _rb.useGravity = true;
        _rb.velocity = finalVelocity;
    }

    private void Reset()
    {
        transform.position = initialPos;
        _rb.velocity = Vector3.zero;
        _rb.useGravity = false;
    }

    private void OnCollisionEnter(Collision collision)
    {

        var temp = GameManager.Instance.ParticlePool.TakeFromPool();
        temp.transform.position = transform.position;
        DamageEnemies();
        ReturnToPool();
    }

    private void DamageEnemies()
    {
        var hits = Physics.OverlapSphere(transform.position, radius, enemy);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out IEnemy enemy))
            {
                enemy.TakeDamage(damagePerShot);
            }
        }
    }

    public void Dispose()
    {
        ReturnToPool();
    }
}
