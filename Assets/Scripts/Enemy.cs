using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public FloatValue maxHealth;

    private void Awake()
    {
        health = maxHealth.intialValue;
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void knock(Rigidbody2D myRigidBody, float knockbackTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidBody, knockbackTime));
        takeDamage(damage);
    }

    // Start is called before the first frame update
    private IEnumerator KnockCo(Rigidbody2D myRigidBody, float knockbackTime)
    {
        if (myRigidBody != null)
        {
            yield return new WaitForSeconds(knockbackTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            myRigidBody.velocity = Vector2.zero;

        }
    }
}
