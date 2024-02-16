using UnityEngine;

public class SphereInteraction : MonoBehaviour
{
    public float baseForceMultiplier = 1f; // Base force to apply for any hit
    public float speedForceMultiplier = 0.5f; // Additional force multiplier for the speed component
    public ScoreManager scoreManager; // Reference to the ScoreManager script

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "GameController")
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 forceDirection = collision.contacts[0].point - transform.position;
            forceDirection = -forceDirection.normalized;

            // Get the velocity of the controller at the moment of collision
            Rigidbody controllerRb = collision.collider.GetComponent<Rigidbody>();
            Vector3 controllerVelocity = controllerRb.velocity;
            float speedFactor = controllerVelocity.magnitude;

            // Calculate the total force as a sum of the base force and an additional speed-based force
            Vector3 force = forceDirection * (baseForceMultiplier + (speedForceMultiplier * speedFactor));

            rb.AddForce(force, ForceMode.Impulse);

            if (scoreManager != null)
            {
                scoreManager.AddScore(1); // Increase score by 1
            }

            Debug.Log($"Applying force: {force}");
            Debug.Log($"New Position: {transform.position}");
        }
    }
}
