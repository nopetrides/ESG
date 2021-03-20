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
    [SerializeField] private string _name;
    [SerializeField] private ThrowableScriptable[] _winsAgainst;
    [SerializeField] private ThrowableScriptable[] _losesAgainst;

    private Dictionary<string, ThrowableScriptable> _winsAgainstRuntime;
    private Dictionary<string, ThrowableScriptable> _losesAgainstRuntime;

    public void OnAfterDeserialize()
    {
        _winsAgainstRuntime = new Dictionary<string, ThrowableScriptable>();
        for (int i = 0; i < _winsAgainst.Length; i++)
        {
            _winsAgainstRuntime.Add(_winsAgainst[i]._name, _winsAgainst[i]);
        }

        _losesAgainstRuntime = new Dictionary<string, ThrowableScriptable>();
        for (int i = 0; i < _losesAgainst.Length; i++)
        {
            _losesAgainstRuntime.Add(_losesAgainst[i]._name, _losesAgainst[i]);
        }
    }

    public Result CalculateResult(ThrowableScriptable opposingHand)
    {
        if (this._name == opposingHand._name)
        {
            return Result.Draw;
        }
        if (_winsAgainstRuntime.ContainsKey(opposingHand._name))
        {
            return Result.Won;
        }
        if (_losesAgainstRuntime.ContainsKey(opposingHand._name))
        {
            return Result.Lost;
        }

        Debug.LogError("chosen hand " + this._name + " does not have a win or lose condition against " +
                       opposingHand._name);
        return Result.Draw;

    }

}
