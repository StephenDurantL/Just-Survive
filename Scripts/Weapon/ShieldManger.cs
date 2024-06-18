using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    public GameObject shieldPrefab; 
    public Transform player; 
    public float rotationSpeed = 100f; 
    public float orbitRadius = 2f; 
    public int weaponLevel = 1; 
    private bool isPerfect = false;

    private GameObject[] shields = new GameObject[4]; 

    void Start()
    {
        InitializeShields();
    }

    void InitializeShields()
    {
        if(weaponLevel>4)
        {
            isPerfect=true;
            weaponLevel=4;
        }
        
        for (int i = 0; i < shields.Length; i++)
        {
            shields[i] = Instantiate(shieldPrefab, CalculatePosition(i * 90), Quaternion.identity);
            shields[i].GetComponent<IronShield>().SetPlayer(player);

            shields[i].SetActive(i < weaponLevel); 
            if(isPerfect){
                shields[i].GetComponentInChildren<IronShield>().IncreaseDamage();
            }
        }
    }

    void Update()
    {
        
        for (int i = 0; i < weaponLevel; i++)
        {
            
            float angle = (Time.time * rotationSpeed + i * 90) % 360;
            float angleRad = angle * Mathf.Deg2Rad;
            Vector3 newPos = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0) * orbitRadius;
            shields[i].transform.position = player.position + newPos;
        }
    }

    Vector3 CalculatePosition(float angle)
    {
        float angleRad = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0) * orbitRadius + player.position;
    }

    public void UpgradeWeaponLevel()
    {
        if (weaponLevel < 4)
        {
            weaponLevel++;
            shields[weaponLevel - 1].SetActive(true);
        }
    }
}
