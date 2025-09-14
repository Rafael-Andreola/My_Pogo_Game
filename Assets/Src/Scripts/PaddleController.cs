using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    [Header("Bar Settings")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    [SerializeField] float padding = 0.5f;


    private Vector2 moveDirection;
    private float borderLimitY;

    private void FixedUpdate()
    {
        transform.Translate(moveSpeed * Time.deltaTime * moveDirection);

        Vector2 vector2 = transform.position;

        vector2.y = Mathf.Clamp(vector2.y, -borderLimitY, borderLimitY); //Limita o movimento da barra no eixo Y

        transform.position = vector2;
    }

    private void Start()
    {
        Camera camera = Camera.main;

        borderLimitY = Camera.main.orthographicSize - padding;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Console.WriteLine("BATEU");

        if (collision.gameObject.CompareTag("Ball"))
        {
            Vector2 hitPoint = collision.contacts[0].point;

            Vector2 paddleCenter = new Vector2(transform.position.x, transform.position.y);
            float difference = paddleCenter.y - hitPoint.y;

            if (hitPoint.y < paddleCenter.y)
            {
                //Hit the lower part of the paddle
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, -difference).normalized * moveSpeed;
            }
            else
            {
                //Hit the upper part of the paddle
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, difference).normalized * moveSpeed;
            }
        }
    }
}
