using UnityEngine;

public class BirdController : MonoBehaviour
{
    public Rigidbody2D birdRigidbody;
    public float jumpForce = 5f;
    public float rotationSpeed = 10f;
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            birdRigidbody.velocity = Vector2.up * jumpForce;
        }
        
        transform.rotation = Quaternion.Euler(0, 0, birdRigidbody.velocity.y * rotationSpeed);
    }
}
