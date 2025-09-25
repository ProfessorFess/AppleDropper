using UnityEngine;

public class TreeController : MonoBehaviour
{
    [Header("Apple Spawning")]
    public GameObject applePrefab;   // Assign in Inspector
    public float spawnInterval = 2f; // Initial seconds between apples
    
    [Header("Movement")]
    public float moveSpeed = 3f;     // Initial horizontal speed
    private float leftBound = -7f;   // Adjust to match camera view
    private float rightBound = 7f;
    private int direction = 1;       // 1 = right, -1 = left
    
    [Header("Difficulty Progression")]
    public float maxMoveSpeed = 8f;      // Maximum tree speed
    public float minSpawnInterval = 0.5f; // Minimum time between apples
    public float difficultyIncreaseRate = 0.1f; // How fast difficulty increases (per second)
    
    private float gameStartTime;
    private float currentSpawnInterval;

    void Start()
    {
        gameStartTime = Time.time;
        currentSpawnInterval = spawnInterval;
        InvokeRepeating("DropApple", 1f, currentSpawnInterval);
    }

    void Update()
    {
        // Calculate difficulty progression based on time
        float timeElapsed = Time.time - gameStartTime;
        float difficultyMultiplier = 1f + (timeElapsed * difficultyIncreaseRate);
        
        // Increase tree movement speed over time
        float currentMoveSpeed = Mathf.Lerp(moveSpeed, maxMoveSpeed, timeElapsed * 0.1f);
        
        // Decrease spawn interval over time (faster apple dropping)
        float newSpawnInterval = Mathf.Lerp(spawnInterval, minSpawnInterval, timeElapsed * 0.1f);
        
        // Update spawn interval if it changed significantly
        if (Mathf.Abs(newSpawnInterval - currentSpawnInterval) > 0.1f)
        {
            currentSpawnInterval = newSpawnInterval;
            CancelInvoke("DropApple");
            InvokeRepeating("DropApple", 0.1f, currentSpawnInterval);
        }
        
        // Move tree left/right with current speed
        transform.Translate(Vector2.right * direction * currentMoveSpeed * Time.deltaTime);

        // Flip direction at bounds
        if (transform.position.x > rightBound) direction = -1;
        if (transform.position.x < leftBound) direction = 1;
    }

    void DropApple()
    {
        Instantiate(applePrefab, transform.position, Quaternion.identity);
    }
}