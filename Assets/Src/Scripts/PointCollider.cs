using JetBrains.Annotations;
using UnityEngine;

public class PointCollider : MonoBehaviour
{
    private void Start()
    {
        transform.localScale = new Vector3(1f, Camera.main.orthographicSize * 2f, 1f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("Ball"))
        {
            FindAnyObjectByType<GameManager>().ResetBall();
            FindAnyObjectByType<UiManager>().AddPlayerScore(1);
        }
    }
}
