using UnityEngine;

public class Ball : MonoBehaviour {
  private Rigidbody2D rb;

  private void Start() {
    // Cache the reference to the rigid body.
    rb = GetComponent<Rigidbody2D>();
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    // Special case hitting the player's paddle.
    if (collision.gameObject.CompareTag("Player")) {
      // Get the difference in x to see if we hit on the left or right.
      // Normalising with the halfWidth gets +1 on the right and -1 on the left.
      float halfWidth = collision.collider.bounds.size.x;
      float x = (transform.position.x - collision.transform.position.x) / halfWidth;

      // Create the new direction (normalised).
      Vector2 direction = new(4 * x, 1);
      direction = direction.normalized;

      // The current speed is the magnitude of the velocity.
      float currentSpeed = rb.velocity.magnitude;

      // Set the new ball velocity.
      rb.velocity = direction * currentSpeed;
    }
  }
}
