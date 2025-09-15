using System.Collections;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] float minAngle;

    private Rigidbody2D rb;

    private float borderLimitY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        borderLimitY = Camera.main.orthographicSize - 0.0f;

        StartCoroutine(ResetAndLaunchBall());
    }

    public IEnumerator ResetAndLaunchBall()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = Vector2.zero;

        yield return new WaitForSeconds(2f);

        ThrowBall();
    }

    private void ThrowBall()
    {
        // Direção aleatória
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);

        Vector2 direction = new Vector2(x, y).normalized;

        // Garante que o X não seja muito pequeno
        if (Mathf.Abs(direction.x) < minAngle)
        {
            // ajusta para o mínimo mantendo o sinal
            direction.x = Mathf.Sign(direction.x) * minAngle;

            // re-normaliza para manter a velocidade constante
            direction = direction.normalized;
        }

        rb.linearVelocity = direction * Speed;
    }

    private void FixedUpdate()
    {
        Vector2 vector2 = transform.position;

        if (vector2.y >= borderLimitY || vector2.y <= -borderLimitY)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -rb.linearVelocity.y).normalized * Speed;
        }
    }

}
