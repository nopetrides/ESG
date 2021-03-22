using UnityEngine;
using UnityEngine.UI;

public class GameModeButton : LabledButton
{
    public void Setup(ThrowablesListScriptables gameMode)
    {
        label.text = gameMode.Description;
    }
}
