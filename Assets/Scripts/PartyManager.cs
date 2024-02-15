using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyManager : MonoBehaviour
{
    public float _maxNinjaHealth = 100f;
    //default _maxRangerHealth is always 100f
    public float _maxRangerHealth = 0f;
    //default _maxMageHealth is always 100f
    public float _maxMageHealth = 0f;
    public float _ninjaHealth;
    public float _rangerHealth;
    public float _mageHealth;
    public float partyHealth;
    //default ninjaDamage is 2 damage per swipe
    public float ninjaDamage = 2f;
    public float rangerDamage = 20f;
    public float mageDamage = 10f;
    private EnemyManager enemyManager;
    [SerializeField] private Button healthPotionButton;
    [SerializeField] private Button buffPotionButton;
    [SerializeField] private Button ninjaAttackButton;
    [SerializeField] private Button rangerAttackButton;
    [SerializeField] private Button MageAttackButton;
    public int healthPotionsAvailable = 5;

    private void Awake()
    {
        _ninjaHealth = _maxNinjaHealth;
        _rangerHealth = _maxRangerHealth;
        _mageHealth = _maxMageHealth;
    }

    private void Update()
    {
        partyHealth = _ninjaHealth + _rangerHealth + _mageHealth;
    }

    public void TakeDamage()
    {
        if (enemyManager.targetPartyMember == 1)
        {
            _ninjaHealth -= enemyManager.enemyDamage;
        }
        else if (enemyManager.targetPartyMember == 2)
        {
            _rangerHealth -= enemyManager.enemyDamage;
        }
        else if (enemyManager.targetPartyMember == 3)
        {
            _mageHealth -= enemyManager.enemyDamage;
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
            else if (partyMemberName == "Ranger")
            {
                _rangerHealth += _maxRangerHealth / 2;
            }
            else if (partyMemberName == "Mage")
            {
                _mageHealth += _maxMageHealth / 2;
            }
        }
        else
        {
            healthPotionButton.interactable = false;
        }
    }
}
