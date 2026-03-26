using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 10f;
    public float laneDistance = 3f;
    public float laneChangeSpeed = 10f;
    public float jumpForce = 5f;
    public float gravity = -20f;

    private int currentLane = 1;
    private float targetX;

    private CharacterController controller;
    private float verticalVelocity;

    public Transform cameraTransform;
    public Vector3 cameraOffset = new Vector3(0, 5, -8);

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        var keyboard = Keyboard.current;

        // INPUT lateral
        if (keyboard.aKey.wasPressedThisFrame || keyboard.leftArrowKey.wasPressedThisFrame)
        {
            if (currentLane > 0) currentLane--;
        }

        if (keyboard.dKey.wasPressedThisFrame || keyboard.rightArrowKey.wasPressedThisFrame)
        {
            if (currentLane < 2) currentLane++;
        }

        targetX = (currentLane - 1) * laneDistance;

        // GRAVEDAD
        if (controller.isGrounded)
        {
            if (verticalVelocity < 0)
                verticalVelocity = -2f;

            if (keyboard.spaceKey.wasPressedThisFrame)
            {
                verticalVelocity = jumpForce;
            }
        }

        verticalVelocity += gravity * Time.deltaTime;

        float moveX = (targetX - transform.position.x) * laneChangeSpeed;

        Vector3 move = new Vector3(moveX, verticalVelocity, forwardSpeed);

        controller.Move(move * Time.deltaTime);
    }

    void LateUpdate()
    {
        if (cameraTransform != null)
        {
            cameraTransform.position = transform.position + cameraOffset;
        }
    }
}