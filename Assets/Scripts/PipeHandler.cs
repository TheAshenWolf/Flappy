using System;
using UnityEngine;

public class PipeHandler : MonoBehaviour
{
    public GameManager gameManager;

    // Called when player passes thru the middle
    public void OnTriggerEnter(Collider other)
    {
        gameManager.AddPoints();
    }

    // Called when player collides with the pipe
    public void OnCollisionEnter(Collision collision)
    {
        gameManager.GameOver();
    }
}