using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class TaskText : MonoBehaviour
{
    public float fadeInDuration;
    public float fadeOutDuration;
    
    [NonSerialized]
    public TextMeshProUGUI textMasTextMeshProUGUI;
    
    public UnityEvent startEvent;
    public UnityEvent updateEvent;

    public static TaskText Singleton {get; set; }

    private void Awake()
    {
        if (!Singleton)
        {
            Singleton = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
        textMasTextMeshProUGUI = gameObject.GetComponent<TextMeshProUGUI>();
        textMasTextMeshProUGUI.DOFade(0f, 0f);
        textMasTextMeshProUGUI.DOFade(1f, fadeOutDuration);
        startEvent.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
