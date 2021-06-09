using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CellContainer : MonoBehaviour
{
    public string name;
    private SpriteRenderer spriteRenderer;

    public UnityEvent choiceTrueEvent;
    public UnityEvent choiceFalseEvent;
    
    public GameObject reloadingPanel;
    
    private CellsHolder cellsHolder;

    public string Name { get => name; set => name = value; }
    
    private void OnMouseDown()
    {
        if (name == TaskText.Singleton.taskName && !reloadingPanel.activeInHierarchy)
        {
            choiceTrueEvent.Invoke();
        }
        else
        {
            if(!reloadingPanel.activeInHierarchy)
              choiceFalseEvent.Invoke();
        }
    }

    void Start()
    {
        cellsHolder = gameObject.GetComponentInParent<CellsHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
