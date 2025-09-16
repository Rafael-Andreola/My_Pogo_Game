using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] Color[] backgroundColor;
    [SerializeField] GameObject PlayerBarPrefab;
    [SerializeField] GameObject BallPrefab;
    [SerializeField] GameObject PointColliderPrefab;


    private Camera mainCamera;
    private GameObject ball;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        mainCamera.backgroundColor = backgroundColor[Random.Range(0, backgroundColor.Length)];

        InstantiatePointColliders();
        InstantiateLeftPaddle();
        InstantiateRightPaddle();
        ResetBall();
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
        leftPaddle.GetComponent<PaddleController>().SetPlayerType(PlayerType.AI);
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
}
