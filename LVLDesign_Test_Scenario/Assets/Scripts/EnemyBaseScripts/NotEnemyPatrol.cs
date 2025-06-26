using UnityEngine;

[RequireComponent(typeof(Patrol))]
public class NotEnemyPatrol : MonoBehaviour
{
    [SerializeField] float spd;
    Patrol patrol;

    private void Awake()
    {
        patrol = GetComponent<Patrol>();
    }

    private void Update()
    {
        patrol.DoPatrol(spd);
    }
}
