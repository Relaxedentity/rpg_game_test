using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Succubus : DevilSlime
{
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;

    private void Update()
    {
        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
    }
    public override void checkDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
            && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk
                && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position,
                    target.position, moveSpeed * Time.deltaTime);

                Vector3 tempVector = target.transform.position - transform.position;
                
                if (canFire)
                {
                    GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                    current.GetComponent<Projectile>().Launch(tempVector);
                    canFire = false;
                }
                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                changeState(EnemyState.walk);
                anim.SetBool("aggro", true);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("aggro", false);
        }
    }
}
