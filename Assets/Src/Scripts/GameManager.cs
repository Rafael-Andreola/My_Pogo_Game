using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] Color[] backgroundColor;
    [SerializeField] GameObject PlayerBarPrefab;
    [SerializeField] GameObject BallPrefab;

    private Camera mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;

        mainCamera.backgroundColor = backgroundColor[Random.Range(0, backgroundColor.Length)];

        GameObject.Instantiate(PlayerBarPrefab, new Vector2(-8f, 0f), Quaternion.identity);

        ResetBall();
    }

    public void ResetBall()
    {
        Destroy(GameObject.FindWithTag("Ball"));
        GameObject.Instantiate(BallPrefab, Vector2.zero, Quaternion.identity);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetBall();
        }
    }
}
