using UnityEngine;
using UnityEngine.InputSystem;

public class BarController : MonoBehaviour
{
    [Header("Bar Settings")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed;

    private float vertical;

    private void FixedUpdate()
    {
        //rb.linearVelocity = new Vector2(vertical * moveSpeed, 0);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKey(KeyCode.W))
            {
                vertical = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                vertical = -1;
            }   
        }
        else
        {
            vertical = 0;
        }

        //rb.linearVelocityY = new Vector2(0, vertical * moveSpeed);
    }
}
