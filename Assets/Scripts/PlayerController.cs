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


    InputAction moveAction;
    Vector3 moveVector;
    Vector2 minBounds;
    Vector2 maxBounds;


    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");

        InitBounds();

    }

    void Update()
    {
        MovePlayer();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void MovePlayer()
    {
        moveVector = moveAction.ReadValue<Vector2>();

        Vector3 newPos = transform.position + moveVector * moveSpeed * Time.deltaTime;

        newPos.x = Math.Clamp(newPos.x, minBounds.x + leftBoundPadding, maxBounds.x - rightBoundPadding);
        newPos.y = Math.Clamp(newPos.y, minBounds.y + bottomBoundPadding, maxBounds.y - topBoundPadding);

        transform.position = newPos;
    }


}
