using System;
using UnityEngine;

namespace JM.TestTask
{
    public interface IMap
    {
        event Action Generated;

        Type TileType { get; }
        Vector3 PlayerSpawnPosition { get; }
        Vector3 Pivot { set; }
        Vector3[] Directions { get; }

        void Generate();

        void Regenerate();
    }
}
