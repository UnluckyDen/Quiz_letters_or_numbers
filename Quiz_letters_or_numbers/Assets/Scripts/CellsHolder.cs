using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using data;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CellsHolder : MonoBehaviour
{
    public UnityEvent startEvent;
    public UnityEvent updateEvent;
    [SerializeField] private LevelsSettings levelsSettings;
    [SerializeField] private List<GameObject> cells;

    private Dictionary<string, Sprite> cellsData = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> curCellsData = new Dictionary<string, Sprite>();
    private List<List<string>> cellsNames = new List<List<string>>();
    private List<List<Sprite>> cellsImages = new List<List<Sprite>>();

    private GameObject reloadingPanel;

    private int difficulty;
    private int numberDataSet;
    public bool isGenerated = false;

    // Start is called before the first frame update
    void Start()
    {
        CreateDataset();
        reloadingPanel = TaskText.Singleton.reloadingPanel;
        reloadingPanel.GetComponent<Image>().DOFade(0.5f, 0);
        reloadingPanel.SetActive(false);
        difficulty = 3;
        //UpdateLevel();
        FirstUpdate();
        startEvent.Invoke();
    }

    private void CreateDataset()
    {
        for (int i = 0; i < levelsSettings.levelsSettingsStructures.Count; i++)
        {
            List<string> listNames = new List<string>();
            listNames.AddRange(levelsSettings.levelsSettingsStructures[i].CellsNames);
            cellsNames.Add(listNames);

            List<Sprite> listImages = new List<Sprite>();
            listImages.AddRange(levelsSettings.levelsSettingsStructures[i].CellsImages);
            cellsImages.Add(listImages);

            // cellsNames.Add(levelsSettings.levelsSettingsStructures[i].CellsNames);
            // cellsImages.Add(levelsSettings.levelsSettingsStructures[i].CellsImages);
        }
    }

    private void FirstUpdate()
    {
        HideCells();
        cellsData.Clear();
        curCellsData.Clear();
        numberDataSet = Random.Range(0, cellsNames.Count);
        LoadCellsData();
        GenerateLevel();
        SetTask();
    }

    private void UpdateLevel()
    {
        StartCoroutine(OneSecond());

        IEnumerator OneSecond()
        {
            yield return new WaitForSeconds(0.5f);
            HideCells();
            cellsData.Clear();
            curCellsData.Clear();
            numberDataSet = Random.Range(0, cellsNames.Count);
            LoadCellsData();
            GenerateLevel();
            SetTask();
        }
    }

    private void LoadCellsData()
    {
        Dictionary<string, Sprite> newData = new Dictionary<string, Sprite>();
        List<string> cellsNamesTemp = cellsNames[numberDataSet];
        List<Sprite> cellsImagesTemp = cellsImages[numberDataSet];
        for (int i = 0; i < cellsNamesTemp.Count && i < cellsImagesTemp.Count; i++)
        {
            cellsData.Add(cellsNamesTemp[i], cellsImagesTemp[i]);
        }
    }

    private void GenerateLevel()
    {
        for (int i = 0; i < difficulty; i++)
        {
            cells[i].SetActive(true);
            KeyValuePair<string, Sprite> keyValuePair = RandomCellSet();
            curCellsData.Add(keyValuePair.Key, keyValuePair.Value);
            cells[i].GetComponentInChildren(typeof(CellContainer)).GetComponent<SpriteRenderer>().sprite =
                curCellsData[keyValuePair.Key];
            cells[i].GetComponentInChildren<CellContainer>().Name = keyValuePair.Key;
        }

        isGenerated = true;
    }

    private KeyValuePair<string, Sprite> RandomCellSet()
    {
        int index = Random.Range(1, cellsData.Count);
        if (cellsData.Count == 1)
        {
            Debug.Log("words is end");
            TaskText.Singleton.taskName = "words is end";
        }

        name = cellsData.Keys.ElementAt(index);
        if (curCellsData.ContainsKey(name))
        {
            Debug.Log(name + " already been added");
        }

        KeyValuePair<string, Sprite> curPair = new KeyValuePair<string, Sprite>(name, cellsData[name]);
        cellsData.Remove(name);
        return curPair;
    }

    private string RandomTask()
    {
        int index = Random.Range(1, curCellsData.Count);
        string name = curCellsData.Keys.ElementAt(index);

        if (TaskText.Singleton.usedNames.Contains(name))
        {
            {
                Debug.Log("YjeBylo");
            }
        }
        else
        {
            TaskText.Singleton.usedNames.Add(name);
        }

        for (int i = 0; i < cellsNames[numberDataSet].Count; i++)
        {
            if (cellsNames[numberDataSet][i] == name)
            {
                cellsNames[numberDataSet].Remove(cellsNames[numberDataSet][i]);
                cellsImages[numberDataSet].Remove(cellsImages[numberDataSet][i]);
                Debug.Log("removed " + name);
                break;
            }
        }

        return name;
    }

    private void SetTask()
    {
        TaskText.Singleton.taskName = RandomTask();
    }

    private void HideCells()
    {
        foreach (var cell in cells)
        {
            cell.GetComponentInChildren<CellContainer>().Name = "NoName";
            cell.SetActive(false);
        }
    }


    public void UpdateDifficulty()
    {
        if (isGenerated)
        {
            difficulty += 3;
            if (difficulty > 9)
            {
                difficulty = 0;
                ShowMenu();
            }
            else if (difficulty == 3)
            {
                FirstUpdate();
            }
            else
            {
                UpdateLevel();
            }
        }
    }

    private void ShowMenu()
    {
        reloadingPanel.SetActive(true);
    }

    public void ReloadGame()
    {
        reloadingPanel.GetComponent<Image>().DOFade(1, 0.1f);
        StartCoroutine(OneSecond());

        IEnumerator OneSecond()
        {
            yield return new WaitForSeconds(1);
            TaskText.Singleton.textMasTextMeshProUGUI.DOFade(0f, 0f);
            UpdateDifficulty();
            TaskText.Singleton.textMasTextMeshProUGUI.DOFade(1f, TaskText.Singleton.fadeOutDuration);
            startEvent.Invoke();
            reloadingPanel.GetComponent<Image>().DOFade(0, 1);
            reloadingPanel.SetActive(false);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}