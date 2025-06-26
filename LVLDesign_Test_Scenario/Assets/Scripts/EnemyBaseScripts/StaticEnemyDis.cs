using UnityEngine;

public class StaticEnemyDis : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] Detector detector;
    [SerializeField] ShootController shoot;
    [SerializeField] Transform spawnPoint;
    
    [Header("Stats")]
    [SerializeField] float cooldown;
    [SerializeField] float radius;
    Vector3 myPos;

    private void Start()
    {
        myPos = transform.position;
    }
    void Update()
    {
        GameObject target = detector.DetectClosestOne(myPos, radius);
        if (target != null)
        {
            Vector3 fwd = target.transform.position - myPos;
            fwd.y = 0;
            transform.forward = fwd.normalized;
            if(!shoot.isOnCooldown) shoot.Shoot(spawnPoint, cooldown);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
