using UnityEngine;

public class GameManager : MonoBehaviour {
  [Header("Unity objects")]
  [SerializeField] private Rigidbody2D ball;
  [SerializeField] private Transform paddle;
  [SerializeField] private TMPro.TextMeshProUGUI scoreText;
  [SerializeField] private GameObject winText;

  [Header("Game parameters")]
  [SerializeField] private float paddleSpeed = 4f;
  [SerializeField] private Vector2 initialBallVelocity = new(2, 4);

  private bool isBallInPlay = false;
  private int score = 0;
  
  private Block[] blocks;
  private int totalBlocks;
  private int blocksRemaining;

  private void Start() {
    // Find all blocks, and count non-destructible blocks.
    blocks = FindObjectsOfType<Block>();
    foreach (Block block in blocks) {
      if (block.isDestructible) {
        totalBlocks++;
      }
    }

    // Start the game with all blocks remaining.
    blocksRemaining = totalBlocks;
  }

  // Public so it can be called from the Blocks.
  public void BlockDestroyed(int blockScore) {
    // Register score.
    score += blockScore;
    scoreText.text = score.ToString();

    // Update the remaining blocks.
    blocksRemaining--;

    // Check for game completion.
    if (blocksRemaining == 0) {
      // Put the ball back on the paddle.
      ResetBall();

      // Show winning text
      winText.SetActive(true);
    }
  }


  public void ResetBall() {
    // Remove all velocity from the ball.
    ball.velocity = Vector2.zero;

    // Ball isn't in play, which sticks it to the paddle.
    isBallInPlay = false;
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
