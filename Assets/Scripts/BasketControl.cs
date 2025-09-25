using UnityEngine;
using UnityEngine.InputSystem;  // if using new Input System

public class BasketController : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
            Debug.LogError("BasketController: No camera tagged 'MainCamera' found!");
    }

    void Update()
    {
        if (mainCamera == null) return;  // prevents crash

        Vector2 mousePos = Mouse.current.position.ReadValue(); 
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCamera.nearClipPlane));
        transform.position = new Vector3(worldPos.x, transform.position.y, transform.position.z);
    }
}