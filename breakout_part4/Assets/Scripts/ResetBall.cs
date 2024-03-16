using UnityEngine;

public class ResetBall : MonoBehaviour {
  [SerializeField] private GameManager gameManager;

  private void OnTriggerEnter2D(Collider2D collision) {
    // When the ball goes out of play tell the GM to reset it.
    gameManager.ResetBall();
  }
}
