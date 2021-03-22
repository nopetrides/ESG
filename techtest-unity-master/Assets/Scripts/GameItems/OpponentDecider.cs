using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A very basic controller for the opponent. Including some simple logic for trying to win against the player.
/// </summary>
public class OpponentDecider
{
    private ThrowableScriptable lastOpponentChoice = null;
    private ThrowableScriptable lastPlayerChoice = null;
    
    public ThrowableScriptable GetOpponentHand(ThrowablesListScriptables gameMode, ThrowableScriptable playersChoice)
    {
        ThrowableScriptable opponentChoice = null;
        
        try
        {
            opponentChoice = gameMode.GetThrowables()[Random.Range(0, gameMode.GetThrowables().Length)];
        }
        catch
        {
            Debug.LogError("Something went wrong with the decision making process");
        }

        lastOpponentChoice = opponentChoice;
        lastPlayerChoice = playersChoice;
        return opponentChoice;
    }
}
