using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleMovementWhitCamFwd : MonoBehaviour
{
    Camera cam;
    CharacterController charController;

    private void Awake()
    {
        cam = Camera.main;
        charController = GetComponent<CharacterController>();
    }

    public void Move(Vector3 dir, float spd)
    {
        if (!charController.isGrounded)
        {
            charController.SimpleMove(Physics.gravity);
        }
        // Convertir la dirección de entrada relativa a la rotación de la cámara
        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;

        // Eliminar componente vertical para evitar movimiento diagonal elevado
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        // Recalcular dirección basada en la cámara
        Vector3 moveDir = dir.z * camForward + dir.x * camRight;

        // Mover al personaje
        charController.SimpleMove(moveDir * spd); // ajusta la velocidad aquí
    }
}
