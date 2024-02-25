using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //enemyStartingHealth is always defaulted to 100
    public float enemyStartingHealth = 100;
    public float enemyHealth;
    //enemyStartingDamage is always defaulted to 10
    public float enemyStartingDamage = 10;
    public float enemyDamage;
    public int targetPartyMember;
    public float roundedHealth;

    public HealthBar healthBar;

    private void Awake()
    {
        enemyHealth = enemyStartingHealth;
        enemyDamage = enemyStartingDamage;
        healthBar.SetMaxHealth(enemyStartingHealth);
    }

    private void FixedUpdate()
    {
        roundedHealth = Mathf.Round(enemyHealth);
        healthBar.SetHealth(enemyHealth);
        if (enemyHealth <= 0)
        {
            enemyStartingDamage += enemyStartingDamage / 2;
            enemyDamage = enemyStartingDamage;
            enemyStartingHealth += enemyStartingHealth / 10 * 2;
            enemyHealth = enemyStartingHealth;
            healthBar.SetHealth(enemyStartingHealth);
        }
    }
}
