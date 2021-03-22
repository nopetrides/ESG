using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the data container for each "hand" the player (and computer) can choose.
/// </summary>
[CreateAssetMenu(fileName = "Throwable_Data", menuName = "Data/New Throwable")]
public class ThrowableScriptable : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _description;
    public string Description => _description;
    [SerializeField] private ThrowableScriptable[] _winsAgainst;
    [SerializeField] private ThrowableScriptable[] _losesAgainst;

    public Result CalculateResult(ThrowableScriptable opposingHand)
    {
        Debug.Log("Player Chose: " + this.Description);
        Debug.Log("Opponent Chose: " + opposingHand.Description);
        if (this._description == opposingHand._description)
        {
            return Result.Draw;
        }
        
        foreach (ThrowableScriptable w in _winsAgainst)
        {
            if (w == opposingHand)
                return Result.Won;
        }
        foreach (ThrowableScriptable l in _losesAgainst)
        {
            if (l == opposingHand)
            return Result.Lost;
        }

        Debug.LogError("chosen hand " + this._description + " does not have a win or lose condition against " +
                       opposingHand._description);
        return Result.Draw;

    }

}
