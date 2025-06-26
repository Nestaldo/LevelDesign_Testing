using UnityEngine;

[RequireComponent(typeof(Patrol))]
public class EnemyBodyPatrol : EnemyBody
{
    [SerializeField] Patrol patrol;
    bool isOnPatrol = true;

    private void Update()
    {
        GameObject attackTarget = detector.DetectClosestOne(transform.position, attackRadius);
        GameObject target = detector.DetectClosestOne(transform.position, detectionRadius);

        if(target != null)
        {
            isOnPatrol = false;
        }

        if(isOnPatrol)
        {
            patrol.DoPatrol(spd);
        }
        else
        {
            if (attackTarget && !isCooldownAttack)
            {
                Attack(attackTarget);
            }
            else if (target)
            {
                MoveTo(target.transform);
            }
            else if(target == null)
            {
                isOnPatrol = true;
            }

        }
        Cooldown();
    }
}
