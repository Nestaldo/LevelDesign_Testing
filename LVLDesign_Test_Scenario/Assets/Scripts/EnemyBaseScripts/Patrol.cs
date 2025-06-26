using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] Transform[] targets;
    [SerializeField] CharacterController charController;
    [SerializeField] float stopDis;
    int targetIndex;
    public void DoPatrol(float spd)
    {
        Vector3 dir = targets[targetIndex].position - charController.transform.position;
        float dis = dir.magnitude;
        if(dis < stopDis)
        {
            targetIndex++;
            if(targetIndex >= targets.Length)
            {
                targetIndex = 0;
            }
        }
        SimpleMove(dir.normalized, spd);
    }

    public void SimpleMove(Vector3 dir, float spd)
    {
        if(!charController.isGrounded)
        {
            charController.SimpleMove(Physics.gravity);
            return;
        }
        dir.y = 0;
        transform.forward = dir;
        charController.SimpleMove(dir * spd);
    }
}
