using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] Color[] backgroundColor;
    [SerializeField] GameObject PlayerBarPrefab;
    [SerializeField] GameObject BallPrefab;
    [SerializeField] GameObject PointColliderPrefab;
    [SerializeField] int ScoreLimit = 3;

    [SerializeField] GameObject mainUI;

    private Camera mainCamera;
    private UiManager uiManager;

    //InGame Objects
    private GameObject ball;
    private PaddleController[] paddles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        uiManager = mainUI.GetComponent<UiManager>();
        mainCamera = Camera.main;
        paddles = new PaddleController[0];
    }

    public void StartGame()
    {
        float camWidth = this.GetCameraWidth();

        InstantiatePaddle(new Vector2((-camWidth/2) + 1f, 0f), PlayerType.Player1, PlayerSide.Left);
        InstantiatePaddle(new Vector2((camWidth/2) - 1f, 0f), PlayerType.AI, PlayerSide.Right);

        ResetMatch();
        InstantiatePointColliders();
    }

    public void ResetMatch()
    {
        mainCamera.backgroundColor = backgroundColor[Random.Range(0, backgroundColor.Length)];

        mainUI.SetActive(true);
        uiManager.ShowInGameUI();

        ResetPaddles();
        ResetBall();
    }

    private void ResetPaddles()
    {
        foreach (PaddleController paddle in paddles)
        {
            Debug.Log(paddle);

            if (paddle.GetPlayerSide() == PlayerSide.Left)
            {
                paddle.gameObject.transform.position = new Vector2(-8f, 0f);
            }
            else
            {
                paddle.gameObject.transform.position = new Vector2(8f, 0f);
            }
        }
    }

    private void InstantiatePointColliders()
    {
        float camWidth = this.GetCameraWidth();

        GameObject rightCollider = Instantiate(PointColliderPrefab, new Vector2(camWidth / 2 + 0.5f, 0f), Quaternion.identity, transform);
        rightCollider.GetComponent<PointCollider>().SetPlayerSide(PlayerSide.Left);

        GameObject leftCollider = Instantiate(PointColliderPrefab, new Vector2(-camWidth / 2 - 0.5f, 0f), Quaternion.identity, transform);
        leftCollider.GetComponent<PointCollider>().SetPlayerSide(PlayerSide.Right);
    }

    private float GetCameraWidth()
    {
        float height = Camera.main.orthographicSize * 2f;
        return height * Camera.main.aspect;
    }

    private void InstantiatePaddle(Vector2 position, PlayerType playerType, PlayerSide playerSide)
    {
        GameObject paddleObject = Instantiate(PlayerBarPrefab, position, Quaternion.identity, transform);

        paddleObject.GetComponent<PaddleController>().SetPaddle(new Paddle(playerType, playerSide));
        paddleObject.name = playerType.ToString();
        paddles.Append(paddleObject.GetComponent<PaddleController>());
    }

    public void ResetBall()
    {
        Destroy(ball);
        ball = Instantiate(BallPrefab, Vector2.zero, Quaternion.identity, transform);
    }

    public GameObject GetBall()
    {
        return ball;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetBall();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            StartGame();
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            ResetMatch();
        }
    }

    public void AddScore(PlayerSide side, int value = 1)
    {
        uiManager.AddScore(side, value);

        if (uiManager.GetScore(side) >= ScoreLimit)
        {
            // Handle game win for the player
            ball.SetActive(false);
            uiManager.ShowWinText(side);
        }
    }
}
