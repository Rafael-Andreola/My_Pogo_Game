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
    private GameObject ball;
    private UiManager uiManager;
    public static GameManager Instance { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiManager = mainUI.GetComponent<UiManager>();
        StartGame();
    }

    private void StartGame()
    {
        mainCamera = Camera.main;
        mainCamera.backgroundColor = backgroundColor[Random.Range(0, backgroundColor.Length)];

        mainUI.SetActive(true);
        uiManager.ShowInGameUI();

        InstantiatePointColliders();
        InstantiateLeftPaddle();
        InstantiateRightPaddle();
        ResetBall();
    }

    private void ResetPaddles()
    {
        PaddleController[] paddles = FindObjectsOfType<PaddleController>();

        foreach (PaddleController paddle in paddles)
        {
            Destroy(paddle.gameObject);
        }

        InstantiateLeftPaddle();
        InstantiateRightPaddle();
    }

    private void InstantiatePointColliders()
    {
        float camHeight = Camera.main.orthographicSize * 2f;
        float camWidth = camHeight * Camera.main.aspect;

        GameObject rightCollider = Instantiate(PointColliderPrefab, new Vector2(camWidth / 2 + 0.5f, 0f), Quaternion.identity, transform);
        rightCollider.GetComponent<PointCollider>().SetPlayerSide(PlayerSide.Left);

        GameObject leftCollider = Instantiate(PointColliderPrefab, new Vector2(-camWidth / 2 - 0.5f, 0f), Quaternion.identity, transform);
        leftCollider.GetComponent<PointCollider>().SetPlayerSide(PlayerSide.Right);
    }

    private void InstantiateLeftPaddle()
    {
        GameObject leftPaddle = Instantiate(PlayerBarPrefab, new Vector2(-8f, 0f), Quaternion.identity, transform);
        leftPaddle.GetComponent<PaddleController>().SetPlayerType(PlayerType.Player1);
    }

    private void InstantiateRightPaddle()
    {
        GameObject rightPaddle = Instantiate(PlayerBarPrefab, new Vector2(8f, 0f), Quaternion.identity, transform);
        rightPaddle.GetComponent<PaddleController>().SetPlayerType(PlayerType.AI);
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

    public void ResetGame()
    {
        uiManager.ResetGameUI();
        ResetBall();
        ResetPaddles();
    }
}
