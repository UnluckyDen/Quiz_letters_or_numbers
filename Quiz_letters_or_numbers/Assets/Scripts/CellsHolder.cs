using System.Collections;
using System.Collections.Generic;
using System.Linq;
using data;
using UnityEngine;
using UnityEngine.Events;

public class CellsHolder : MonoBehaviour
{
    public UnityEvent startEvent;
    public UnityEvent updateEvent;

    [SerializeField] private LevelsSettings levelsSettings;
    [SerializeField] List<GameObject> cells;

    private Dictionary<string,Sprite> cellsData = new Dictionary<string, Sprite>();
    private Dictionary<string,Sprite> curCellsData = new Dictionary<string, Sprite>();

    private List<string> usedNames = new List<string>();

    private int difficulty;
    private int numberDataSet;

    // Start is called before the first frame update
    void Start()
    {
        difficulty = 3;
        foreach (var cell in cells)
        {
            cell.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        }
        UpdateLevel();
        startEvent.Invoke();
    }
    
    void UpdateLevel()
    {
        numberDataSet = Random.Range(0, levelsSettings.levelsSettingsStructures.Count);
        LoadCellsData();
        curCellsData.Clear();
        GenerateLevel();
        SetTask();
    }

    private void LoadCellsData()
    {
        Dictionary<string, Sprite> newData = new Dictionary<string, Sprite>();
        List<string> cellNames = levelsSettings.levelsSettingsStructures[numberDataSet].CellsNames;
        List<Sprite> cellsImages = levelsSettings.levelsSettingsStructures[numberDataSet].CellsImages;

        for (int i = 0; i < cellNames.Count && i < cellsImages.Count; i++)
        {
            cellsData.Add(cellNames[i], cellsImages[i]);
        }
    }

    private void GenerateLevel()
    {
        for (int i = 0; i < difficulty; i++)
        {
            cells[i].transform.localScale = new Vector3(1f, 1f, 0f);
            KeyValuePair<string, Sprite> keyValuePair = RandomCellSet();
            curCellsData.Add(keyValuePair.Key,keyValuePair.Value);
            GameObject container = cells[i].gameObject.GetComponentInChildren(typeof(CellContainer)).gameObject;
            container.GetComponent<SpriteRenderer>().sprite = curCellsData[keyValuePair.Key];
            container.GetComponent<CellContainer>().Name = keyValuePair.Key;
        }
    }

    private KeyValuePair<string,Sprite> RandomCellSet()
    {
        int index = Random.Range(1,cellsData.Count);
        string name = cellsData.Keys.ElementAt(index);
        if (curCellsData.ContainsKey(name))
        {
            Debug.Log(name + " already been added");
            return RandomCellSet();
        }
        Debug.Log(name);
        KeyValuePair<string, Sprite> curPair = new KeyValuePair<string, Sprite> (name,cellsData[name]);
        return curPair;
    }

    private string RandomTask()
    {
        int index = Random.Range(1,curCellsData.Count);
        string name = curCellsData.Keys.ElementAt(index);
        if (usedNames.Contains(name))
        {
            if (usedNames.Count == cellsData.Count)
            {
                Debug.Log("Cap,none used words is end");
            }
            return RandomTask();
        }
        else
        {
            usedNames.Add(name);
        }
        return name;
    }

    private void SetTask()
    {
        TaskText.Singleton.textMasTextMeshProUGUI.text ="Find " + RandomTask();
    }
}