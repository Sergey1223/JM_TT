using System;
using UnityEngine;

namespace JM.TestTask
{
    [Serializable]
    public class BonusItemSettings
    {
        [SerializeField]
        [Range(0.01f, 1f)]
        private float _spawnProbability;

        [SerializeField]
        private CrystalBonusItemSettings _crystalBonusItemSettings;

        public float SpawnProbability { get => _spawnProbability; }

        public CrystalBonusItemSettings CrystalBonusSettings { get => _crystalBonusItemSettings; }
    }
}
