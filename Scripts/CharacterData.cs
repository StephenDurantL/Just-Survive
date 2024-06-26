using UnityEngine;

[CreateAssetMenu(fileName = "Character Data", menuName = "Scriptable Object/Character Data", order = int.MaxValue )]
public class CharacterData : ScriptableObject
{
    [SerializeField] Sprite sprite;
    [SerializeField] RuntimeAnimatorController controller;
    [SerializeField] CharacterType characterType;
    [SerializeField] int healthPoint;
    [SerializeField] int attackPower;
    [SerializeField] int defencePower;
    [SerializeField] int speed;

    public enum CharacterType
    {
        monster0,
        monster1,
        monster2,
        monster3,
        Player,
        monster4
    }

    public int GetHealthPoint()
    {
        return healthPoint;
    }

    public int GetAttackPower()
    {
        return attackPower;
    }

    public int GetDefencePower()
    {
        return defencePower;
    }

    public int GetSpeed()
    {
        return speed;
    }

    public CharacterType GetCharacterType()
    {
        return characterType;   
    }

    public RuntimeAnimatorController GetController()
    {
        return controller;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }
}