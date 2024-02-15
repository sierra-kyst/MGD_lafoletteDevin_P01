using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToStateInteraction : MonoBehaviour
{
    public static Button swordBtn;
    public static Button sniperBtn;
    public static Button spellBtn;

    private GamePlayState playState;

    private void Awake()
    {
        swordBtn = GameObject.Find("Sword_btn").GetComponent<Button>();
        //sniperBtn = GameObject.Find("Sniper_btn").GetComponent<Button>();
        //spellBtn = GameObject.Find("Spell_btn").GetComponent<Button>();
    }

    private void Update()
    {
        swordBtn.onClick.AddListener(playState.Sword);
        //sniperBtn.onClick.AddListener(playState.Sniper);
        //spellBtn.onClick.AddListener(playState.Spell);
    }
}
