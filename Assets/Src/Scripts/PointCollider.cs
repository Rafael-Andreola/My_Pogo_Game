using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointCollider : MonoBehaviour
{
    [SerializeField] PlayerSide playerSide;

    private UiManager uiManager;

    private void Start()
    {
        transform.localScale = new Vector3(1f, Camera.main.orthographicSize * 2f, 1f);

        uiManager = FindFirstObjectByType<UiManager>();
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

    public void SetPlayerSide(PlayerSide side)
    {
        playerSide = side;
    }
    
    private void AddPoint()
    {
        if (playerSide == PlayerSide.Left)
        {
            uiManager.AddLeftScore(1);
        }
        else if (playerSide == PlayerSide.Right)
        {
            uiManager.AddRightScore(1);
        }
    }
}
