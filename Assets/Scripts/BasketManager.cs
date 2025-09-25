using UnityEngine;
using UnityEngine.SceneManagement;

public class BasketManager : MonoBehaviour
{
    [Header("Assign your baskets in order (bottom to top)")]
    public GameObject[] baskets; // Array of basket objects in the scene

    private int basketIndex = 0; // Tracks which basket is next to remove

    // Singleton pattern so Apple.cs can call LoseBasket easily
    public static BasketManager instance;

    void Awake()
    {
        // Make sure instance is set
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Call this when the player misses an apple
    public void LoseBasket()
{
    if (basketIndex < baskets.Length)
    {
        Destroy(baskets[basketIndex]);
        basketIndex++;
    }

    if (basketIndex >= baskets.Length)
    {
        GameOver();
    }
}

    void GameOver()
    {
        Debug.Log("Game Over!");
        // Restart the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}