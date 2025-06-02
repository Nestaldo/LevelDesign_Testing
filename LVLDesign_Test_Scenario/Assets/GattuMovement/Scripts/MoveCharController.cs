using System.Collections.Generic;
using UnityEngine;

public class MoveCharController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] CharacterController charController;

    [Header("Move Settings")]
    public float spd;
    public float rootSpd;
    public float gravity;
    
    [Header("Jump Settings")]
    [SerializeField] bool enableJump;
    [SerializeField] KeyCode keyToJump;
    public float jumpHeight;
    public float jumpTimeToUp;
    public float jumpTimeToDown;
    
    float x;
    float z;
    Vector3 dir;
    
    bool jumpRequeriment;
    bool onJump;
    float jumpStartTime;
    float lastJumpHeight;
    Vector3 jumpStartPos;

    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        if(enableJump && !jumpRequeriment && !onJump && Input.GetKeyDown(keyToJump))
        {
            jumpRequeriment = true;
        }
    }

    void FixedUpdate()
    {
        if (z != 0)
        {
            dir = transform.forward * z * spd * Time.fixedDeltaTime;
        }
        else dir = Vector3.zero;

        if (x != 0)
        {
            transform.Rotate(Vector3.up * x * rootSpd * Time.fixedDeltaTime);
        }

        if(!charController.isGrounded)
        {
            dir.y = -1 * gravity * Time.fixedDeltaTime;
        }

        if(jumpRequeriment)
        {
            StartJump();
        }

        if (onJump)
        {
            JumpState();
        }
        else
        {
            charController.Move(dir);
        }
    }

    void StartJump()
    {
        onJump = true;
        jumpRequeriment = false;
        jumpStartTime = Time.time;
        lastJumpHeight = 0;
        jumpStartPos = transform.position;
    }

    void JumpState()
    {
        float t = Time.time - jumpStartTime;
        float newHeight = GetJumpHeight(t, jumpTimeToUp, jumpTimeToDown, jumpHeight);
        float totalTime = jumpTimeToUp + jumpTimeToDown;
        float deltaY = newHeight - lastJumpHeight;

        lastJumpHeight = newHeight;

        if (charController.isGrounded && t > jumpTimeToUp)
        {
            EndJump();
            return;
        }

        if (charController.collisionFlags == CollisionFlags.Above)
        {
            EndJump();
            return;
        }

        if(t >= totalTime)
        {
            EndJump();
            return;
        }

        dir.y = deltaY;
        charController.Move(dir);
    }

    void EndJump()
    {
        onJump = false;
    }

    float GetJumpHeight(float t, float tUp, float tDown, float hMax)
    {
        if (t < 0f)
            return 0f;

        if (t < tUp)
        {
            // Subida: desde 0 a hMax
            float g = 2f * hMax / (tUp * tUp);
            float v0 = g * tUp;
            return v0 * t - 0.5f * g * t * t;
        }
        else if (t < tUp + tDown)
        {
            // Bajada: desde hMax a 0
            float tFall = t - tUp;
            float g = 2f * hMax / (tDown * tDown);
            return hMax - 0.5f * g * tFall * tFall;
        }
        return -gravity * Time.fixedDeltaTime;
    }

    void OnControllerColliderHit(ControllerColliderHit col)
    {
        switch (col.controller.collisionFlags)
        {
            case CollisionFlags.None:
                //Debug.Log("Simple coll");
                break;
            case CollisionFlags.Sides:
                //Debug.Log("Side Coll");
                break;
            case CollisionFlags.Above:
                //Debug.Log("Arriba Coll");
                break;
            case CollisionFlags.Below:
                //Debug.Log("Abajo Coll");
                break;
            default:
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if (jumpTimeToUp <= 0 || jumpTimeToDown <= 0 || jumpHeight <= 0 || spd <= 0)
            return;

        float totalTime = jumpTimeToUp + jumpTimeToDown;
        float resolution = 0.01f;
        int count = Mathf.CeilToInt(totalTime / resolution);

        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i <= count; i++)
        {
            float t = i * resolution;

            Vector3 point = transform.position;
            if (onJump) point = jumpStartPos;
            point += transform.forward * spd * t;
            point.y += GetJumpHeight(t, jumpTimeToUp, jumpTimeToDown, jumpHeight);

            points.Add(point);
        }

        for (int i = 0; i < points.Count - 1; i++)
        {
            Gizmos.DrawLine(points[i], points[i + 1]);
        }
    }
}

