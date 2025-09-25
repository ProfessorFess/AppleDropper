using UnityEngine;

public class Apple : MonoBehaviour
{
    private bool hasHit = false; // prevent multiple triggers

    void Start()
    {
        Debug.Log("Apple script started on: " + gameObject.name);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Apple collided with: {collision.gameObject.name}, Tag: {collision.gameObject.tag}");
        
        if (hasHit) return; // already processed this apple

        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Apple hit ground!");
            BasketManager.instance.LoseBasket();
            Destroy(gameObject);
            hasHit = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Apple triggered with: {other.gameObject.name}, Tag: {other.gameObject.tag}");
        
        if (hasHit) return; // already processed this apple

        if (other.gameObject.CompareTag("Basket"))
        {
            Debug.Log("Apple caught by basket!");
            ScoreManager.instance.AddScore(1); // add a point
            Destroy(gameObject);
            hasHit = true;
        }
    }
}