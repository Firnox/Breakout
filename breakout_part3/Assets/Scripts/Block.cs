using UnityEngine;

public class Block : MonoBehaviour {
  [Header("Block parameters")]
  [SerializeField] private int hitsToDestroy = 1;
  [SerializeField] private bool isDestructible = true;
  [SerializeField] private int score = 10;

  private GameManager gameManager;

  private void Start() {
    // Cache the reference to our game manager.
    gameManager = FindObjectOfType<GameManager>();
  }

  // Handle collisions with the block.
  private void OnCollisionEnter2D(Collision2D collision) {
    // Only care if the block is destructible.
    if (isDestructible) {
      // Register the hit, by decrementing our counter.
      hitsToDestroy--;

      // If it's been hit enough times destroy it and register score.
      if (hitsToDestroy == 0) {
        gameManager.BlockDestroyed(score);
        gameObject.SetActive(false);
      }
    }
  }

}
