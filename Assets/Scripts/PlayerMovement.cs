using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { 
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;
    public FloatValue currentHealth;
    public SignalSender playerHealthSignal;
    public VectorValue startingPosition;
    public GameObject projectile;
    
    void Start()
    {
        transform.position = startingPosition.initialValue;
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (myRigidBody.velocity.magnitude > 0)
        {
            Debug.Log(myRigidBody.velocity.magnitude);
        }
        if (currentState == PlayerState.walk )
        {
            myRigidBody.velocity = Vector2.zero;
        }
    }


    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger )
        {
            StartCoroutine(AttackCO());
        }
        else if (Input.GetButtonDown("Second Weapon") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(SecondAttackCO());
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        UpdateAnimationAndMove();
    }
    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);

        }
    }
    private IEnumerator AttackCO()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
    }
    void MoveCharacter()
    {
        myRigidBody.MovePosition(
            transform.position + change * speed * Time.deltaTime
            );
    }
    public void knock(float knockbackTime, float damage)
    {
        currentHealth.runtimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.runtimeValue > 0)
        {
 
            StartCoroutine(KnockCo(knockbackTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
    private IEnumerator KnockCo( float knockbackTime)
    {
        if (myRigidBody != null)
        {
            yield return new WaitForSeconds(knockbackTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            yield return new WaitForSeconds(.4f);

        }
        myRigidBody.velocity = Vector2.zero;
    }
    private IEnumerator SecondAttackCO()
    {
        //animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        MakeSpirit();
        //animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
    }

    private void MakeSpirit()
    {
        Vector2 temp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        SpiritProjectile spirit = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<SpiritProjectile>();
        spirit.Setup(temp, ChooseSpiritDirection());
    }

    Vector3 ChooseSpiritDirection()
    {
        float temp = Mathf.Atan2(animator.GetFloat("moveX"), animator.GetFloat("moveY")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp); 
    }

}
