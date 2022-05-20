using System;
using UnityEngine;

namespace JM.TestTask
{
    public interface IPlayer
    {
        Vector3 Position { get; }

        event Action TileActivated;
        event Action BonusCollected;
        event Action LeftMap;
        event Action Died;

        void Respawn();

        void StartMoving();

        void SwitchDirection();

        void Fall();
    }
}
