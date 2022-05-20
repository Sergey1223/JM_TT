using System;
using UnityEngine;

namespace JM.TestTask
{
    [Serializable]
    public class ScoreCounterSettings
    {
        [SerializeField]
        private int _tilePrice;

        [SerializeField]
        private int _bonusPrice;

        public int TilePrice { get => _tilePrice; }

        public int BonusPrice { get => _bonusPrice; }

    }
}