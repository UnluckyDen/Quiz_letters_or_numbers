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

    [NonSerialized] public string taskName;
    
    public UnityEvent startEvent;
    public UnityEvent updateEvent;
    public List<string> usedNames;
    public GameObject reloadingPanel;
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
        reloadingPanel = GameObject.Find("Panel");
        textMasTextMeshProUGUI = gameObject.GetComponent<TextMeshProUGUI>();
        textMasTextMeshProUGUI.DOFade(0f, 0f);
        textMasTextMeshProUGUI.DOFade(1f, fadeOutDuration);
        startEvent.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        textMasTextMeshProUGUI.text = "Find " + taskName;
    }
}
