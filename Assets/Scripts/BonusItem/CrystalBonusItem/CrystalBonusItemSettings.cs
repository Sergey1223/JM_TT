using System;
using UnityEngine;

namespace JM.TestTask
{
    [Serializable]
    public class CrystalBonusItemSettings
    {
        [SerializeField]
        [Range(0.01f, 1f)]
        private float _probabilityDensity;

        [SerializeField]
        [Tooltip("Position ralative to tile center position.")]
        private Vector3 _relativePosition;

        [SerializeField]
        private GameObject _prefab;

        public float ProbabilityDensity { get => _probabilityDensity; }

        public Vector3 RelativePosition { get => _relativePosition; }

        public GameObject Prefab { get => _prefab; }
    }
}
