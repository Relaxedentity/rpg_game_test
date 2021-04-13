using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image[] health;
    public Sprite healthBar;
    public FloatValue healthContainers;
    public FloatValue playerCurrentHealth;
    // Start is called before the first frame update
    void Start()
    {
        initHealth();
    }

    public void initHealth()
    {
        for (int i = 0; i < healthContainers.intialValue; i++)
        {
            health[i].gameObject.SetActive(true);
            health[i].sprite = healthBar;
        }
    }
    public void updateHearts()
    {
        float tempHealth = playerCurrentHealth.runtimeValue;
        for(int i = 0; i < healthContainers.intialValue; i++)
        {
            if(i<= tempHealth-1)
            {
                health[i].sprite = healthBar;
            }
            else if(i>= tempHealth)
            {
                health[i].gameObject.SetActive(false);
            }
        }
    }

}
