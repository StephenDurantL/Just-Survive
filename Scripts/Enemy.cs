using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    new Rigidbody2D rigidbody;
    EnemyMovement enemyMove;
    private bool isDead = false;
    [SerializeField] private int experienceValue = 10; 

    void Awake()
    {
        Initialize();

    }
    protected override void Initialize()
    {
        base.Initialize();
        enemyMove = GetComponent<EnemyMovement>();

        
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public override void ReduceHealthPoint(int damage)
    {
        base.ReduceHealthPoint(damage);

        
        if (hitCoroutine == null && GetHealthPoint() > 0)
        {
            hitCoroutine = StartCoroutine(UnderAttack());
        }

       
        if (GetHealthPoint() > 0)
        {
            KnockBack();
        }
    }

    void KnockBack()
    {
        
        if (rigidbody != null)
        {
            
            rigidbody.AddForce(enemyMove.GetDirection() * -2f, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogError("Rigidbody is not assigned on " + gameObject.name);
        }
    }


    

    public override void Die()
    {
        if (isDead) return;
        isDead = true;
        StartCoroutine(DieAnimation());
        Player.GetInstance().GainExperience(experienceValue);
    }

    protected override IEnumerator DieAnimation()
    {

        gameObject.SetActive(false);
        
        
        yield break;  
    }

    protected override IEnumerator UnderAttack()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.2f);

        
    }
}
