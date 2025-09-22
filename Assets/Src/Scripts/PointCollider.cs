using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointCollider : MonoBehaviour
{
    [SerializeField] PlayerSide playerSide;

    private GameManager gameManager;

    private void Start()
    {
        transform.localScale = new Vector3(1f, Camera.main.orthographicSize * 2f, 1f);

        gameManager = FindFirstObjectByType<GameManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            FindAnyObjectByType<GameManager>().ResetBall();
            AddPoint();
        }
    }

    private void AddPoint()
    {
        gameManager.AddScore(playerSide);
    }

    public void SetPlayerSide(PlayerSide side)
    {
        playerSide = side;
    }
}
