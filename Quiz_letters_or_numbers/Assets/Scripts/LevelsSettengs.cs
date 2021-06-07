using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

namespace data
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "LevelSettings", order = 0)]
    public class LevelsSettings: ScriptableObject
    {
        [Serializable]
        public struct LevelsSettingsStructure
        {
            [SerializeField] private List<Sprite> cellsImage;
            [SerializeField] private List<string> cellsName;

            public List<Sprite> CellsImages => cellsImage;
            public List<string> CellsNames => cellsName;
        }
        public List<LevelsSettingsStructure> levelsSettingsStructures;
    }
}