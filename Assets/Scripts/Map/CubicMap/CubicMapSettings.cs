using System;
using UnityEngine;

namespace JM.TestTask
{
    [Serializable]
    public class CubicMapSettings
    {
        [SerializeField]
        private GameObject _playerSpawnPosition;

        [SerializeField]
        private GameObject _generationSourcePoint;

        [SerializeField]
        private Vector3 _bounds;

        [SerializeField]
        private Vector3 _startPosition;

        [SerializeField]
        private Vector3 _startPlatformScale;

        [SerializeField]
        private GameObject _tilePrefab;

        [SerializeField]
        private float _tileFallDelay;

        [SerializeField]
        private int _tileFallTime;

        public Vector3 PlayerSpawnPosition { get => _playerSpawnPosition.transform.position; }

        public Vector3 GenerationSourcePoint { get => _generationSourcePoint.transform.position; }

        public Vector3 Bounds { get => _bounds; }

        public Vector3 StartPosition { get => _startPosition; }

        public Vector3 StartPlatformScale { get => _startPlatformScale; }

        public GameObject TilePrefab { get => _tilePrefab; }

        public float TileFallDelay { get => _tileFallDelay; }

        public float TileFallTime { get => _tileFallTime; }
    }
}
