using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMenuController : MonoBehaviour
{
    [SerializeField] private GameModeButton modeButtonPrefab = null;
    [SerializeField] private GameModeScriptable gameModeData = null;
    [SerializeField] private Transform buttonsParent = null;

    private void Start()
    {
        SetupGameModeButtons();
    }

    private void SetupGameModeButtons()
    {
        foreach (var gameMode in gameModeData.GameModes)
        {
            GameModeButton button = Instantiate(modeButtonPrefab, buttonsParent, false);
            button.Setup(gameMode);
            button.Clickable.onClick.AddListener(delegate
            {
                ButtonGameModeSelected(gameMode);
            });
        }

        modeButtonPrefab.gameObject.SetActive(false);
    }

    private void ButtonGameModeSelected(ThrowablesListScriptables gameMode)
    {
        gameModeData.LastSelectedGameMode = gameMode;
        SceneManager.LoadScene("BRPS");
    }
}
