using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Detector))]
public class EnemyBody : MonoBehaviour
{
    [SerializeField] protected float detectionRadius;
    [SerializeField] protected float attackRadius;
    [SerializeField] protected float spd;
    [SerializeField] float attackCooldown;
    [SerializeField] float dmg;
    [SerializeField] protected Detector detector;
    [SerializeField] CharacterController charController;
    protected bool isCooldownAttack;
    float startAttackTime;

    private void Update()
    {
        GameObject attackTarget = detector.DetectClosestOne(transform.position, attackRadius);
        GameObject target = detector.DetectClosestOne(transform.position, detectionRadius);
        if(attackTarget && !isCooldownAttack)
        {
            Attack(attackTarget);
        }
        else if(target)
        {
            MoveTo(target.transform);
        }

        Cooldown();
    }

    protected void Attack(GameObject target)
    {
        isCooldownAttack = true;
        startAttackTime = Time.time;
        target.GetComponent<DamagingObject>().GetDamage(dmg);
    }

    protected void MoveTo(Transform target)
    {
        if (!charController.isGrounded)
        {
            charController.SimpleMove(Physics.gravity);
            return;
        }
        Vector3 dir = (target.position - transform.position).normalized;
        dir.y = 0;
        transform.forward = dir;
        charController.SimpleMove(dir * spd);        
    }

    protected void Cooldown()
    {
        if (isCooldownAttack)
        {
            float time = Time.time - startAttackTime;
            if (time < attackCooldown)
            {
                isCooldownAttack = false;
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

