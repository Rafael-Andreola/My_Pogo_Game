using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BarController : MonoBehaviour
{
    [Header("Bar Settings")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    [SerializeField] Camera camera;


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

        if(transform.position.y <= 4.47f && transform.position.y >= -4.47f)
        {
            transform.Translate(moveSpeed * Time.deltaTime * moveDirection);
        }
        else if (transform.position.y > 4.47)
        {
            transform.position = new Vector2(transform.position.x, 4.47f);
        }
        else if (transform.position.y < -4.47)
        {
            transform.position = new Vector2(transform.position.x, -4.47f);
        }
    }
}
