using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableButton : LabledButton
{
    public void Setup(ThrowableScriptable throwable)
    {
        label.text = throwable.Description;
    }
}
