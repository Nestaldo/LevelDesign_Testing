using UnityEngine;

public class EnemyDisAndPatrol : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float detectionRadius;
    [SerializeField] float attackRadius;
    [SerializeField] float spd;
    [SerializeField] float cooldown;
    
    [Header("Dependecies")]
    [SerializeField] Patrol patrol;
    [SerializeField] Detector detector;
    [SerializeField] ShootController shoot;
    [SerializeField] Transform spawnPos;
    bool isAttacking;

    private void Update()
    {
        GameObject detectTarget = detector.DetectClosestOne(transform.position, detectionRadius);
        if(detectTarget != null)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        if (!isAttacking)
        {
            patrol.DoPatrol(spd);
        }
        else
        {
            Vector3 myPos = transform.position;
            Vector3 detectedPos = detectTarget.transform.position;
            float disToTarget = Vector3.Distance(myPos, detectedPos);
            if(!shoot.isOnCooldown && disToTarget <= attackRadius)
            {
                shoot.Shoot(spawnPos, cooldown);
            }
            else
            {
                if(disToTarget > attackRadius) patrol.SimpleMove((detectedPos - myPos).normalized, spd);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
