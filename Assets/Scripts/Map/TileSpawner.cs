using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace JM.TestTask
{
    public class TileSpawner : Spawner
    {
        private CubicMapSettings _settings;
        private CubicTileFactory _tileFactory;

        [Inject]
        public void Construct(CubicMapSettings settings, CubicTileFactory tileFactory)
        {
            _settings = settings;
            _tileFactory = tileFactory;
        }

        public CubicTile Spawn(Vector3 position)
        {
            CubicTile tile = Dequeue() as CubicTile;

            if (tile == null)
            {
                tile = _tileFactory.Create();
            }

            tile.transform.position = position;
            tile.FallDelay = _settings.TileFallDelay;
            tile.FallTime = _settings.TileFallTime;

            return tile;
        }

        public CubicTile Spawn(Vector3 position, Vector3 scale)
        {
            CubicTile tile = Spawn(position);

            tile.transform.localScale = scale;

            return tile;
        }

        public void Despawn(CubicTile tile)
        {
            Enqueue(tile);
        }
    }
}
