using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Ground")]
    public Transform[] ground;
    
    [Header("Pipes")]
    public Transform[] pipes;
    
    [Header("UI")]
    public TextMeshProUGUI pointsText;
    
    [Header("Settings")]
    public float speed = 2f;
    public float pipeMaxY = 2f;
    public float pipeMinY = -2f;
    
    [Header("Data")]
    public int points = 0;

    public virtual void AddPoints(int amount = 1)
    {
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
        // Handle ground
        foreach (Transform groundTransform in ground)
        {
            groundTransform.localPosition += Vector3.left * (Time.deltaTime * speed);
            if (!(groundTransform.localPosition.x < -14f)) continue;
            Vector3 position = groundTransform.localPosition;
            position-= Vector3.left * 30f;
            groundTransform.localPosition = position;
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

    public void GameOver()
    {
        Debug.Log("Game Over");
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
