using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace JM.TestTask
{
    public class CubicMap : IMap, IInitializable
    {
        private readonly Vector3[] _directions = new Vector3[] { Vector3.right, Vector3.forward };

        private CubicMapSettings _settings;
        private TileSpawner _tileSpawner;
        private MainBonusFactory _bonusFactory;
        private Vector3 _pivot;
        private Vector3 _generationPoint;
        private Vector3 _tileSize;
        private List<CubicTile> _nonActivatedTiles;
        private List<CubicTile> _activatedTiles;
        private List<IBonusItem> _nonActivatedBonuses;

        public Type TileType => typeof(CubicTile);
        public Vector3 PlayerSpawnPosition { get => _settings.PlayerSpawnPosition; }
        public Vector3 Pivot { set => _pivot = value; }
        public Vector3[] Directions { get => _directions; }

        public event Action Generated;

        private void InitializeTileSize()
        {
            if (_settings.TilePrefab.TryGetComponent(out Renderer renderer))
            {
                // Ignore Y coordinate.
                _tileSize = new Vector3(renderer.bounds.size.x, 0, renderer.bounds.size.z);
            }
            else
            {
                Debug.Log("Could not initialize tile size (prefab has no renderer).");
            }
        }

        private bool TryGenerateTilePosition(Vector3 source, out Vector3 result)
        {
            List<Vector3> positions = new List<Vector3>(_directions.Length);

            foreach (Vector3 direction in _directions)
            {
                if (IsAvailableDirection(source, direction, out Vector3 target))
                {
                    positions.Add(target);
                }
            }

            if (positions.Count > 0)
            {
                result = positions[UnityEngine.Random.Range(0, positions.Count)];

                return true;
            }

            result = source;

            return false;
        }

        private bool IsAvailableDirection(Vector3 source, Vector3 direction, out Vector3 target)
        {
            target = new Vector3();

            float xCoordinate = source.x + _tileSize.x * direction.x;
            float zCoordinate = source.z + _tileSize.z * direction.z;

            if (Mathf.Abs(xCoordinate) > _pivot.x + _settings.Bounds.x || Mathf.Abs(zCoordinate) > _pivot.z + _settings.Bounds.z)
            {
                return false;
            }

            // Ignore Y coordiante.
            target.x = xCoordinate;
            target.y = source.y;
            target.z = zCoordinate;

            return true;
        }

        private void GenerateCell(Vector3 position, Vector3 scale, bool spawnBonus)
        {
            CubicTile tile = _tileSpawner.Spawn(position, scale);

            tile.FallDelay = _settings.TileFallDelay;
            tile.FallTime = _settings.TileFallTime;
            tile.Activated += OnTileActivated;
            tile.Destroyed += OnTileDestroyed;

            _nonActivatedTiles.Add(tile);

            if (spawnBonus)
            {
                SpawnBonusItem(position);
            }
        }

        private void SpawnBonusItem(Vector3 position)
        {
            IBonusItem bonusItem = _bonusFactory.CreateByProbability(position);

            if (bonusItem != null)
            {
                _nonActivatedBonuses.Add(bonusItem);

                bonusItem.Activated += OnBonusActivated;
            }
        }

        private void OnTileActivated(CubicTile tile)
        {
            if (TryGenerateTilePosition(_generationPoint, out Vector3 target))
            {
                GenerateCell(target, Vector3.one, true);

                _generationPoint = target;
            }

            _nonActivatedTiles.Remove(tile);
            _activatedTiles.Add(tile);
        }
        
        private void OnTileDestroyed(CubicTile tile)
        {
            _activatedTiles.Remove(tile);

            _tileSpawner.Despawn(tile);
        }
        
        private void OnBonusActivated(IBonusItem bonus)
        {
            _nonActivatedBonuses.Remove(bonus);
        }

        [Inject]
        public void Construct(CubicMapSettings settings, TileSpawner tileSpawner, MainBonusFactory bonusFactory)
        {
            _settings = settings;
            _tileSpawner = tileSpawner;
            _bonusFactory = bonusFactory;
        }

        public void Initialize()
        {
            _pivot = _settings.PlayerSpawnPosition;
            _generationPoint = _settings.GenerationSourcePoint;
            _nonActivatedTiles = new List<CubicTile>();
            _activatedTiles = new List<CubicTile>();
            _nonActivatedBonuses = new List<IBonusItem>();

            InitializeTileSize();
        }

        public void Generate()
        {
            GenerateCell(_settings.StartPosition, _settings.StartPlatformScale, false);

            Vector3 source;
            Vector3 target = _generationPoint;

            do
            {
                GenerateCell(target, Vector3.one, true);

                source = target;
            }
            while (TryGenerateTilePosition(source, out target));

            _generationPoint = source;

            Generated?.Invoke();
        }

        public void Regenerate()
        {
            _pivot = _settings.PlayerSpawnPosition;
            _generationPoint = _settings.GenerationSourcePoint;

            foreach (CubicTile tile in _nonActivatedTiles)
            {
                _tileSpawner.Despawn(tile);
            }

            _nonActivatedTiles.Clear();

            foreach (CubicTile tile in _activatedTiles)
            {
                tile.Reset();

                _tileSpawner.Despawn(tile);
            }

            _activatedTiles.Clear();

            foreach (IBonusItem bonus in _nonActivatedBonuses)
            {
                bonus.Destroy();
            }

            _nonActivatedBonuses.Clear();

            Generate();
        }

        public Type GetTyleType()
        {
            return typeof(CubicTile);
        }
    }
}
