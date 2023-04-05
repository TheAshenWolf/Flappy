using UnityEngine;

public class BirdController : MonoBehaviour
{
    public Rigidbody2D birdRigidbody;
    public float flapForce = 5f;
    public float rotationSpeed = 10f;
    public GameManager gameManager;
    
    public virtual void StartGame()
    {
        gameManager.gameStarted = true;
        birdRigidbody.isKinematic = false;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (gameManager.gameEnded) return;
            
            if (!gameManager.gameStarted) StartGame();
            birdRigidbody.velocity = Vector2.up * flapForce;
        }
        
        // Rotate the bird according to the velocity, so it dips down when falling and points up when flapping its wings
        transform.rotation = Quaternion.Euler(0, 0, birdRigidbody.velocity.y * rotationSpeed);
    }
    
    // Called when player passes thru the middle between pipes
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        gameManager.AddPoints();
    }
    
    // Called when player collides the ground/pipes
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager.GameOver();
    }
}
