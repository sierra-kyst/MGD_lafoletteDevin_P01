using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyManager : MonoBehaviour
{
    public float _maxNinjaHealth = 100f;
    public float _ninjaHealth;
    //default ninjaDamage is 2 damage per swipe
    public float ninjaDamage = 2f;
    private EnemyManager enemyManager;
    [SerializeField] private Button healthPotionButton;
    [SerializeField] private Button buffPotionButton;
    [SerializeField] private Button ninjaAttackButton;
    [SerializeField] private Button rangerAttackButton;
    [SerializeField] private Button MageAttackButton;
    public int healthPotionsAvailable = 5;

    public HealthBar healthBar;

    private void Awake()
    {
        _ninjaHealth = _maxNinjaHealth;
        healthBar.SetMaxHealth(_maxNinjaHealth);
    }

    private void Update()
    {
        healthBar.SetHealth(_ninjaHealth);
    }

    public void TakeDamage()
    {
        if (enemyManager.targetPartyMember == 1)
        {
            _ninjaHealth -= enemyManager.enemyDamage;
        }
    }

    public void UseHealthPotion(string partyMemberName)
    {
        if (healthPotionsAvailable > 0)
        {
            healthPotionButton.interactable = true;
            if (partyMemberName == "Ninja")
            {
                _ninjaHealth += _maxNinjaHealth / 2;
            }
        }
        else
        {
            healthPotionButton.interactable = false;
        }
    }
}
