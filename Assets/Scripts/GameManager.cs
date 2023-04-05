using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Ground")]
    public Transform[] ground;
    
    [Header("Pipes")]
    public Transform[] pipes;
    
    [Header("UI")]
    public TextMeshProUGUI pointsText;
    public GameObject endGameScreen;
    public TextMeshProUGUI highScoreText;
    
    [Header("Settings")]
    public float speed = 2f;
    public float pipeMaxY = 2f;
    public float pipeMinY = -2f;
    
    [Header("Data")]
    public int points = 0;
    public bool gameEnded = false;
    public bool gameStarted = false;
    
    
    public virtual void AddPoints(int amount = 1)
    {
        if (gameEnded) return;
        points += amount;
        pointsText.text = points.ToString();
    }
    
    public virtual void Start()
    {        
        for (int index = 0; index < pipes.Length; index++)
        {
            if (index == 0) continue;
            Transform pipe = pipes[index];
            Vector3 position = pipe.localPosition;
            position = new Vector3(position.x, Random.Range(pipeMinY, pipeMaxY), position.z);
            pipe.localPosition = position;
        }
    }
    
    public virtual void Update()
    {
        // if the game hasn't started yet or already ended, dont move anything
        if (!gameStarted || gameEnded) return;
        
        // Handle ground
        foreach (Transform groundTransform in ground)
        {
            groundTransform.position += Vector3.left * (Time.deltaTime * speed);
            if (!(groundTransform.position.x < -16f)) continue;
            Vector3 position = groundTransform.position;
            position-= Vector3.left * 32f;
            groundTransform.position = position;
        }
        
        // Handle pipes
        foreach (Transform pipe in pipes)
        {
            pipe.localPosition += Vector3.left * (Time.deltaTime * speed);
            if (!(pipe.localPosition.x < -16f)) continue;
            Vector3 position = pipe.localPosition;
            position = new Vector3(position.x + 32f, Random.Range(pipeMinY, pipeMaxY), position.z);
            pipe.localPosition = position;
        }
    }

    public virtual void GameOver()
    {
        gameEnded = true;
        
        // Overwrite the high score if the current score is higher
        PlayerPrefs.SetInt("highScore", Mathf.Max(PlayerPrefs.GetInt("highScore", 0), points));
        endGameScreen.gameObject.SetActive(true);
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highScore", 0);
    }
    
    public virtual void RestartGame()
    {
        // This is the fastest way to restart the game, since we are not keeping any data between scenes
        SceneManager.LoadScene(0);
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(-20, pipeMaxY + 5 + 2, 0), new Vector3(20, pipeMaxY + 5 + 2, 0));
        Gizmos.DrawLine(new Vector3(-20, pipeMinY + 5 - 2, 0), new Vector3(20, pipeMinY + 5 - 2, 0));
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(-20, pipeMaxY + 5, 0), new Vector3(20, pipeMaxY + 5, 0));
        Gizmos.DrawLine(new Vector3(-20, pipeMinY + 5, 0), new Vector3(20, pipeMinY + 5, 0));
    }
#endif
}
