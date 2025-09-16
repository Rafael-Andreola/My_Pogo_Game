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
    [SerializeField] float angleMagnitude;


    PlayerType player;
    private Vector2 moveDirection;
    private float borderLimitY;

    private void FixedUpdate()
    {

        if(player == PlayerType.AI)
        {
            GameObject ball = GameObject.FindFirstObjectByType<GameManager>().GetBall();
            
            if (ball != null)
            {
                if (ball.transform.position.y > transform.position.y + padding)
                {
                    moveDirection = Vector2.up;
                }
                else if (ball.transform.position.y < transform.position.y - padding)
                {
                    moveDirection = Vector2.down;
                }
                else
                {
                    moveDirection = Vector2.zero;
                }
            }
        }

        transform.Translate(moveSpeed * Time.deltaTime * moveDirection);

        Vector2 vector2 = transform.position;

        vector2.y = Mathf.Clamp(vector2.y, -borderLimitY, borderLimitY); //Limita o movimento da barra no eixo Y

        transform.position = vector2;
    }

    private void Start()
    {
        Camera camera = Camera.main;

        borderLimitY = Camera.main.orthographicSize - padding;

        if (player == PlayerType.AI)
        {
            moveSpeed = moveSpeed / 2; //AI moves slower than player
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void SetPlayerType(PlayerType playerType)
    {
        player = playerType;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Vector2 paddleCenter = new Vector2(transform.position.x, transform.position.y);
            float difference = (paddleCenter.y - collision.transform.position.y) * angleMagnitude;

            Rigidbody2D ballRb = collision.GetComponent<Rigidbody2D>();

            if (collision.transform.position.y < paddleCenter.y)
            {
                //Hit the lower part of the paddle
                ballRb.linearVelocity = new Vector2(-ballRb.linearVelocity.x, difference).normalized * collision.gameObject.GetComponent<BallMovement>().getVelocity();
            }
            else
            {
                //Hit the upper part of the paddle
                ballRb.linearVelocity = new Vector2(-ballRb.linearVelocity.x, difference).normalized * collision.gameObject.GetComponent<BallMovement>().getVelocity();
            }
        }
    }
}
