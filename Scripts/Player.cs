using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
public class Player : Character
{
    [SerializeField] Slider hpSlider;
    [SerializeField] GameObject GameOverWindow;
    [SerializeField] private Slider expSlider; 
    [SerializeField] private TextMeshProUGUI levelText;    
    [SerializeField] private GameObject rewardWindow; 
    [SerializeField] private Button[] rewardButtons; 
    [SerializeField] private TextMeshProUGUI[] rewardTexts;
    [SerializeField] List<GameObject> weaponManagers;
    static Player instance;
    float attackSpeed;

    bool isColliding;
    private bool isDead; 
    private int currentLevel = 1;
    private int currentExp = 0;
    private int expToNextLevel = 100; 

    
    private Player() { }

    public enum RewardType
    {
        RecoverHealth,
        IncreaseAttackPower,
        IncreaseAttackSpeed,
        IncreaseMoveSpeed,
        EnhancedWeapon,
        AddWeapon
    }
    private List<RewardType> allRewards = new List<RewardType>
    {
        RewardType.RecoverHealth,
        RewardType.IncreaseAttackPower,
        RewardType.IncreaseAttackSpeed,
        RewardType.IncreaseMoveSpeed,
        RewardType.EnhancedWeapon,
        RewardType.AddWeapon
    };

    void Awake()
    {
        Initialize();
        rewardWindow.SetActive(false);

        for (int i = 0; i < rewardButtons.Length; i++)
        {
            int index = i;
            rewardButtons[i].onClick.AddListener(() => ChooseReward(index));
        }

        foreach (GameObject manager in weaponManagers)
        {
            manager.SetActive(false);
        }

        
        ActivateRandomWeaponManager();
    }

    protected override void Initialize()
    {
        base.Initialize();
        instance = this;
        attackSpeed = 1f;
        isColliding = false;
        isDead = false; 
        hpSlider.maxValue = GetHealthPoint();
        hpSlider.value = GetHealthPoint();
        GameOverWindow.SetActive(false);
        expSlider.maxValue = expToNextLevel;
        expSlider.value = currentExp;
        UpdateLevelText(); 
    }

    public static Player GetInstance()
    {
        return instance;
    }

    public void GainExperience(int exp)
    {
        currentExp += exp;
        expSlider.value = currentExp;

        // 判断是否达到升级条件
        if (currentExp >= expToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLevel++;
        currentExp -= expToNextLevel; 
        expToNextLevel = Mathf.CeilToInt(expToNextLevel * 1.2f); 
        expSlider.maxValue = expToNextLevel;
        expSlider.value = currentExp;

        ShowRewardWindow();
        RecoverHealthPoint(1);

        UpdateLevelText();
    }

    private void UpdateLevelText()
    {
        
        levelText.text = $"LV {currentLevel}";
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }

    public bool IsDead()
    {
        return isDead; 
    }

    public void IncreaseAttackSpeed(float value)
    {
        attackSpeed -= value;
    }



    public override void Die()
    {
        isDead = true; 
        if (GetAnimator() != null)
        {
            GetAnimator().SetTrigger("DieTrigger");  
        }
        StartCoroutine(DieAnimation());  
    }

    protected override IEnumerator DieAnimation()
    {
        yield return new WaitForSeconds(1.0f); 

        gameObject.SetActive(false);
        GameOverWindow.SetActive(true);
    }

    protected override IEnumerator UnderAttack()
    {
        spriteRenderer.color = Color.red;

        do
        {
            isColliding = false;
            yield return new WaitForSeconds(0.2f);
        }
        while (isColliding);

        spriteRenderer.color = Color.white;
        hitCoroutine = null;
    }

    public override void ReduceHealthPoint(int damage)
    {
        base.ReduceHealthPoint(damage);
        isColliding = true;
        hpSlider.value = GetHealthPoint();

        if (hitCoroutine == null)
        {
            hitCoroutine = StartCoroutine(UnderAttack());
        }
    }
    public override void RecoverHealthPoint(int amount)
    {
        base.RecoverHealthPoint(amount);
        hpSlider.value = GetHealthPoint();

    }

    void ActivateRandomWeaponManager()
    {
        
        if (weaponManagers.Count > 0)
        {
            
            int randomIndex = Random.Range(0, weaponManagers.Count);

            
            weaponManagers[randomIndex].SetActive(true);
        }
    }

    public void ShowRewardWindow()
    {
        
        rewardWindow.SetActive(true);
        Time.timeScale = 0f;

        
        List<RewardType> selectedRewards = GetRandomRewards(3);

        for (int i = 0; i < rewardButtons.Length; i++)
        {
            rewardTexts[i].text = GetRewardDescription(selectedRewards[i]);
            rewardButtons[i].gameObject.SetActive(true);
        }
    }

    private List<RewardType> GetRandomRewards(int count)
    {
        List<RewardType> availableRewards = new List<RewardType>(allRewards);

        bool hasInactiveWeaponManagers = weaponManagers.Exists(manager => !manager.activeSelf);

        
        if (!hasInactiveWeaponManagers)
        {
            availableRewards.Remove(RewardType.AddWeapon);
        }

        List<RewardType> selectedRewards = new List<RewardType>();

        for (int i = 0; i < count; i++)
        {
            
            if (availableRewards.Count == 0)
            {
                break;
            }

            
            int randomIndex = Random.Range(0, availableRewards.Count);
            selectedRewards.Add(availableRewards[randomIndex]);
            availableRewards.RemoveAt(randomIndex);
        }

        return selectedRewards;
    }


    private string GetRewardDescription(RewardType rewardType)
    {
        switch (rewardType)
        {
            case RewardType.RecoverHealth:
                return "Recover Health";
            case RewardType.IncreaseAttackPower:
                return "Increase Attack Power";
            case RewardType.IncreaseAttackSpeed:
                return "Increase Attack Speed";
            case RewardType.IncreaseMoveSpeed:
                return "Increase Move Speed";
            case RewardType.EnhancedWeapon:
                return "Enhanced Weapon";
            case RewardType.AddWeapon:
                return "Add Weapon";
            default:
                return "Unknown Reward";
        }
    }

    private void ChooseReward(int index)
    {
        
        RewardType chosenReward = (RewardType)System.Enum.Parse(typeof(RewardType), rewardTexts[index].text.Replace(" ", ""));
        ApplyRewardEffect(chosenReward);

        rewardWindow.SetActive(false);
        Time.timeScale = 1f;
    }

    private void ApplyRewardEffect(RewardType rewardType)
    {
        switch (rewardType)
        {
            case RewardType.RecoverHealth:
                RecoverHealthPoint(8);
                break;
            case RewardType.IncreaseAttackPower:
                IncreaseAttackPower(1);
                break;
            case RewardType.IncreaseAttackSpeed:
                IncreaseAttackSpeed(0.2f);
                break;
            case RewardType.IncreaseMoveSpeed:
                IncreaseSpeed(10);
                break;
            case RewardType.EnhancedWeapon:           
                Enhancedweapons();
                break;
            case RewardType.AddWeapon:
                AddWeapon();
                break;
        }
    }
    private void Enhancedweapons()
    {
        
        foreach (GameObject manager in weaponManagers)
        {
            if (manager.activeSelf)
            {
                
                var method = manager.GetComponent<MonoBehaviour>()?.GetType().GetMethod("UpgradeWeaponLevel");

                if (method != null)
                {
                    
                    method.Invoke(manager.GetComponent<MonoBehaviour>(), null);
                }
                
            }
        }
    }


    private void AddWeapon()
    {
        
        List<GameObject> inactiveWeaponManagers = new List<GameObject>();

        
        foreach (GameObject manager in weaponManagers)
        {
            if (!manager.activeSelf)
            {
                inactiveWeaponManagers.Add(manager);
            }
        }

        
        if (inactiveWeaponManagers.Count > 0)
        {
            
            int randomIndex = Random.Range(0, inactiveWeaponManagers.Count);

           
            inactiveWeaponManagers[randomIndex].SetActive(true);
        }
        else
        {
            Debug.LogWarning("所有武器管理器都已激活，没有新的武器可添加。");
        }
    }


}
