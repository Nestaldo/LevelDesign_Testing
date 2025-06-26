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
        // Convertir la direcci�n de entrada relativa a la rotaci�n de la c�mara
        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;

        // Eliminar componente vertical para evitar movimiento diagonal elevado
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        // Recalcular direcci�n basada en la c�mara
        Vector3 moveDir = dir.z * camForward + dir.x * camRight;

        // Mover al personaje
        charController.SimpleMove(moveDir * spd); // ajusta la velocidad aqu�
    }
}
