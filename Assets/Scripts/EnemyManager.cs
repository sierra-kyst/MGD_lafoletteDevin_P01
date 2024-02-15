using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //enemyStartingHealth is always defaulted to 100
    public float enemyStartingHealth = 3;
    public float enemyHealth;
    //enemyStartingDamage is always defaulted to 10
    public float enemyStartingDamage = 10;
    public float enemyDamage;
    public int targetPartyMember;

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
