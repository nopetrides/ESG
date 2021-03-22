using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameModeData", menuName = "Data/New Game Mode Data")]
public class GameModeScriptable : ScriptableObject
{
    [SerializeField] private ThrowablesListScriptables[] gameModes;

    public ThrowablesListScriptables[] GameModes => gameModes;

    private ThrowablesListScriptables lastSelectedGameMode;

    public ThrowablesListScriptables LastSelectedGameMode
    {
        get => lastSelectedGameMode;
        set => lastSelectedGameMode = value;
    }
}
