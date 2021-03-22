using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeButton : MonoBehaviour
{
    [SerializeField] private Text label = null;
    [SerializeField] private Button clickable = null;
    public Button Clickable => clickable;
    public void Setup(ThrowablesListScriptables gameMode)
    {
        label.text = gameMode.Description;
    }
}
