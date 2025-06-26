using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] float spd;
    [SerializeField] float dmg;
    [SerializeField] string detectionTag;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        rb.linearVelocity = transform.forward * spd;
    }

    public void ResetBullet(Vector3 pos, Quaternion root)
    {
        gameObject.SetActive(true);
        transform.position = pos;
        transform.rotation = root;
        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = transform.forward * spd;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag(detectionTag))
        {
            collision.collider.GetComponent<DamagingObject>().GetDamage(dmg);
        }
        gameObject.SetActive(false);
    }
}
