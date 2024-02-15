using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToStateInteraction : MonoBehaviour
{
    public static Button endlessMode;

    GameSetupState setupState;

    private void Awake()
    {
        endlessMode = GameObject.Find("EndlessMode_btn").GetComponent<Button>();
    }

    private void Update()
    {
        endlessMode.onClick.AddListener(setupState.GetOut);
    }
}
