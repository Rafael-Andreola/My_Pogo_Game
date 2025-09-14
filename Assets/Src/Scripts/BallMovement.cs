using System.Collections;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] float Speed;
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
        float yrandom = Random.Range(-1f, 1f);
        float xrandom = Random.Range(-1f, 1f);


        float y = yrandom == 0 ? -1f : yrandom;
        float x = xrandom == 0 ? -1f : xrandom;

        rb.linearVelocity = new Vector2(x, y).normalized * Speed;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector2 vector2 = transform.position;

        if (vector2.y >= borderLimitY || vector2.y <= -borderLimitY)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -rb.linearVelocity.y).normalized * Speed;
        }
    }
}
