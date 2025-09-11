using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BarController : MonoBehaviour
{
    [Header("Bar Settings")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed;

    private Vector2 moveDirection;

    //private void FixedUpdate()
    //{
    //    rb.linearVelocity = new Vector2(vertical * moveSpeed, 0);
    //}

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime * moveDirection);
    }
}
