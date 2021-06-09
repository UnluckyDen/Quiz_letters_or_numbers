using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResetButton : MonoBehaviour
{
    public UnityEvent click;

    private void OnMouseDown()
    {
        click.Invoke();
    }
}
