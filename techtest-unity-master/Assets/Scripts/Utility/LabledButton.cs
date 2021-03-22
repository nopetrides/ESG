using UnityEngine;
using UnityEngine.UI;

public abstract class LabledButton : MonoBehaviour
{
    [SerializeField] protected Text label = null;
    [SerializeField] protected Button clickable = null;
    public Button Clickable => clickable;
}
