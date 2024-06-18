using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText; // Text component displaying the timer
    [SerializeField] private GameObject gameOverUI; // Game over UI screen
    [SerializeField] private GameObject specialEnemyManager1; // First special enemy manager
    [SerializeField] private GameObject specialEnemyManager2; // Second special enemy manager
    [SerializeField] private GameObject EnemyManager;
    [SerializeField] private GameObject EnemyManager1;
    [SerializeField] private GameObject EnemyManager2;

    private float remainingTime = 5*60; // Initial countdown time is 5 minutes

    private bool isCountingDown = false;


    void Start()
    {
        // Initialize timer text and hide game over UI
        UpdateTimerText();
        gameOverUI.SetActive(false);

        // Initially disable both special enemy managers
        if (specialEnemyManager1 != null)
            specialEnemyManager1.SetActive(false);
        if (specialEnemyManager2 != null)
            specialEnemyManager2.SetActive(false);
        if (EnemyManager1!=null)
            EnemyManager1.SetActive(false);
        if (EnemyManager2!=null)
            EnemyManager2.SetActive(false);    

        // Start countdown
        isCountingDown = true;
    }

    void Update()
    {
        if (isCountingDown)
        {
            // Decrease remaining time every frame
            remainingTime -= Time.deltaTime;

            // When countdown finishes, show the game over UI
            if (remainingTime <= 0)
            {
                remainingTime = 0; // Avoid negative values
                isCountingDown = false;
                GameOver();
            }
            if (remainingTime <= 4*60)
            {
                if (specialEnemyManager1 != null)
                    specialEnemyManager1.SetActive(true);
                
                EnemyManager.SetActive(false);
            }

            
            if (remainingTime <= 3*60)
            {
                if (specialEnemyManager1 != null)
                    specialEnemyManager1.SetActive(true);
                
                EnemyManager1.SetActive(true);
            }

            
            if (remainingTime <= 2*60)
            {
                if (specialEnemyManager2 != null)
                    specialEnemyManager2.SetActive(true);
                

                EnemyManager1.SetActive(false);
            }

            if (remainingTime <= 60)
            {
                if (specialEnemyManager2 != null)
                    specialEnemyManager2.SetActive(true);
                
                EnemyManager2.SetActive(true);
            }


            // Update timer text
            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        // Convert seconds to minutes and seconds format, and update to text component
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = $"{minutes:D2}:{seconds:D2}";
    }

    private void GameOver()
    {
        // Show game over UI
        gameOverUI.SetActive(true);
        Time.timeScale = 0; // Pause the game
    }
}
