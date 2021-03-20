using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the class for each gamemode, which references a set of Throwable Scriptable Objects
/// </summary>
[CreateAssetMenu(fileName = "Throwable_List", menuName = "Data/New Game Mode")]
public class ThrowablesListScriptables : ScriptableObject
{
    [SerializeField] private string _name;
    public string Name => _name;
    [SerializeField] private ThrowableScriptable[] _throwablesInMode;

    public ThrowableScriptable[] GetThrowables()
    {
        return _throwablesInMode;
    }
}
