using UnityEngine;

public class CollisionTestiing : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        print("collision");
        if(collision.collider.CompareTag("Player"))
        {
            Debug.Log("PlayerDetected");
        }
    }
}
