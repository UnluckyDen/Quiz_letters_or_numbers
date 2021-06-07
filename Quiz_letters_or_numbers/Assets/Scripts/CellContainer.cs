using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CellContainer : MonoBehaviour
{
    private bool isTrueAnswer;
    private string name;
    private SpriteRenderer spriteRenderer;

    public UnityEvent choiceTrueEvent;
    
    public bool IsTrueAnsver => isTrueAnswer;

    public string Name { get => name; set => name = value; }
    
    private void OnMouseDown()
    {
        choiceTrueEvent.Invoke();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
