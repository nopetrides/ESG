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

    private int sameChoiceCounter;
    private int sameChoiceCeiling;
    
    /// <summary>
    /// Very simple AI, keeps track of what the player is doing, then tries to "beat" the player.
    /// If it always played the winning hand, that would be unfair
    /// If it always played the opposite of what the player did in the last turn, that would be too easy to manipulate
    /// We want to allow the opponent to be able to throw the same hand multiple times in a row, since it should feel random
    /// If the player continues to use hte same hand, the computer will eventually compensate (when it "clues in" is random)
    /// </summary>
    /// <param name="gameMode"></param>
    /// <param name="playersChoice"></param>
    /// <returns></returns>
    public ThrowableScriptable GetOpponentHand(ThrowablesListScriptables gameMode, ThrowableScriptable playersChoice)
    {
        ThrowableScriptable opponentChoice = null;
        
        if (playersChoice == lastPlayerChoice)
        {
            if (sameChoiceCounter < 1)
            {
                sameChoiceCeiling = Random.Range(2, 5); // choose an amount of rounds to "clue in"
            }

            sameChoiceCounter++;
        }
        else if (playersChoice != lastPlayerChoice)
        {
            sameChoiceCounter = 0; // reset only if the player stops repeating the move
        }
        if (playersChoice == lastPlayerChoice && sameChoiceCounter >= sameChoiceCeiling)
        {
            ThrowableScriptable[] losesAgainst = playersChoice.LosesAgainst;
            opponentChoice = losesAgainst[Random.Range(0, losesAgainst.Length)]; // choose a random winning hand
        }
        else {
            try
            {
                opponentChoice = gameMode.Throwables[Random.Range(0, gameMode.Throwables.Length)];
            }
            catch
            {
                Debug.LogError("Something went wrong with the decision making process");
            }
        }

        lastOpponentChoice = opponentChoice;
        lastPlayerChoice = playersChoice;
        return opponentChoice;
    }
}
