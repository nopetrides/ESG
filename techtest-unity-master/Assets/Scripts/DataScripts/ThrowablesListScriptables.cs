using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the class for each gamemode, which references a set of Throwable Scriptable Objects
/// </summary>
[CreateAssetMenu(fileName = "Throwable_List", menuName = "Data/New Throwables In Game Mode")]
public class ThrowablesListScriptables : ScriptableObject
{
    [SerializeField] private string _description;
    public string Description => _description;
    [SerializeField] private ThrowableScriptable[] _throwablesInMode;    
    public ThrowableScriptable[] Throwables => _throwablesInMode;
    [SerializeField] private Sprite _rulesSprite;
    public Sprite RulesSprite => _rulesSprite;


}
