using UnityEngine;
using TMPro; // Using TextMeshPro for text handling

public class FloatingDamageText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageText;
    public float floatSpeed = 1f;
    public float lifetime = 1.5f;

    // Set the damage value to display
    public void SetDamageValue(int damage)
    {
        damageText.text = damage.ToString();
    }

    void Start()
    {
        // Destroy the object after its lifetime expires
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move the text upward to create a floating effect
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }
}
