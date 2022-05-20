using System;
using UnityEngine;

namespace JM.TestTask
{
    [Serializable]
    public class BallPlayerSettings
    {
        [SerializeField]
        private GameObject _playerPrefab;

        [SerializeField]
        private float _speed;

        [SerializeField]
        private float _fallTime;

        public GameObject PlayerPrefab { get => _playerPrefab; }

        public float Speed { get => _speed; }

        public float FallTime { get => _fallTime; }
    }
}
