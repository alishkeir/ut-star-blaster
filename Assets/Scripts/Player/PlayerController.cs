using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;

    [SerializeField] float leftBoundPadding = 1f;
    [SerializeField] float rightBoundPadding = 1f;
    [SerializeField] float topBoundPadding = 1f;
    [SerializeField] float bottomBoundPadding = 1f;

    Shooter playerShooter;

    Vector3 moveVector;
    Vector2 minBounds;
    Vector2 maxBounds;


    void Start()
    {

        playerShooter = GetComponent<Shooter>();

        // fireAction = InputSystem.actions.FindAction("Fire");
        // moveAction = InputSystem.actions.FindAction("Move");

        InitBounds();

    }

    void Update()
    {
        HandleMovement();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerShooter.isFiring = true;
        }
        else
        {
            playerShooter.isFiring = false;
        }
    }


    void HandleMovement()
    {
        Vector3 newPos = transform.position + moveSpeed * Time.deltaTime * moveVector;

        newPos.x = Math.Clamp(newPos.x, minBounds.x + leftBoundPadding, maxBounds.x - rightBoundPadding);
        newPos.y = Math.Clamp(newPos.y, minBounds.y + bottomBoundPadding, maxBounds.y - topBoundPadding);

        transform.position = newPos;
    }


    void OnDrawGizmos()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera == null)
            return;

        float distance = Mathf.Abs(transform.position.z - mainCamera.transform.position.z);

        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 topLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, distance));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, distance));
        Vector3 bottomRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, distance));

        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);

        Gizmos.color = Color.red;

        bottomLeft.x += leftBoundPadding;
        bottomLeft.y += bottomBoundPadding;

        topLeft.x += leftBoundPadding;
        topLeft.y -= topBoundPadding;

        topRight.x -= rightBoundPadding;
        topRight.y -= topBoundPadding;

        bottomRight.x -= rightBoundPadding;
        bottomRight.y += bottomBoundPadding;

        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
    }




}
