using UnityEngine;
using UnityEngine.UI;

public class HealthBarFollow : MonoBehaviour
{
    public Transform target; // The character's Transform
    public Vector3 offset = new Vector3(0, -1, 0); // Offset between the health bar and the character
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Convert the character's world position to UI coordinates
        Vector3 worldPosition = target.position + offset;
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        
        // Update the position of the health bar
        rectTransform.position = screenPosition;
    }
}
