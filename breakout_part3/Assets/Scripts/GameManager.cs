using UnityEngine;

public class GameManager : MonoBehaviour {
  [Header("Unity objects")]
  [SerializeField] private Rigidbody2D ball;
  [SerializeField] private Transform paddle;
  [SerializeField] private TMPro.TextMeshProUGUI scoreText;

  [Header("Game parameters")]
  [SerializeField] private float paddleSpeed = 4f;
  [SerializeField] private Vector2 initialBallVelocity = new(2, 4);

  private bool isBallInPlay = false;
  private int score = 0;

  // Public so it can be called from the Blocks.
  public void BlockDestroyed(int blockScore) {
    // Register score.
    score += blockScore;
    scoreText.text = score.ToString();
  }

  void Update() {
    // Paddle movement.
    float direction = Input.GetAxis("Horizontal");
    float moveAmount = direction * paddleSpeed * Time.deltaTime;
    paddle.Translate(moveAmount * Vector2.right);

    // Ensure paddle stays in play area.
    if (paddle.position.x < -5) {
      paddle.position = new Vector2(-5, paddle.position.y);
    } else if (paddle.position.x > 5) {
      paddle.position = new Vector2(5, paddle.position.y);
    }

    // Launching the ball 
    if (!isBallInPlay) {
      // Stick the ball to the paddle.
      ball.transform.position = paddle.position + (Vector3.up * 0.2f);

      // Lanuch the ball on fire.
      if (Input.GetButtonDown("Fire1")) {
        isBallInPlay = true;
        ball.AddForce(initialBallVelocity, ForceMode2D.Impulse);
      }
    }
  }
}
