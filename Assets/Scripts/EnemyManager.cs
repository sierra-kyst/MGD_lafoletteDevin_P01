using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float enemyStartingHealth = 100;
    private float enemyHealth;
    public float enemyStartingDamage = 10;
    private float enemyDamage;

    private void Awake()
    {
        enemyHealth = enemyStartingHealth;
        enemyDamage = enemyStartingDamage;
    }

    private void FixedUpdate()
    {
        if(enemyHealth <= 0)
        {
            enemyStartingDamage += enemyStartingDamage / 2;
            enemyDamage = enemyStartingDamage;
            enemyStartingHealth += enemyStartingHealth / 10 * 2;
            enemyHealth = enemyStartingHealth;
        }
    }
}
