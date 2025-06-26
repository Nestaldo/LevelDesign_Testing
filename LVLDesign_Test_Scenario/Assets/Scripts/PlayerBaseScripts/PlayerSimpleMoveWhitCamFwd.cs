using UnityEngine;

public class PlayerSimpleMoveWhitCamFwd : MonoBehaviour
{
    [SerializeField] SimpleMovementWhitCamFwd movement;
    [SerializeField] float spd;
    float x;
    float z;
    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        if(z != 0 || x != 0 )
        {
            movement.Move(new Vector3(x,0,z), spd);
        }
    }
}
