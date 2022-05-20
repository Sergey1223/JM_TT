using UnityEngine;
using Zenject;

namespace JM.TestTask
{
    public class PositionUpdatingService : IInitializable, ILateTickable
    {
        private Camera _camera;
        private IMap _map;
        private IPlayer _player;
        private Vector3 _moveDirection;
        private Vector3 _cameraDistance;

        private void UpdateCameraPosition()
        {
            Vector3 position = Vector3.Project(_player.Position - _cameraDistance, _moveDirection);

            // Ignore Y coordinate.
            _camera.transform.position = new Vector3(position.x, _camera.transform.position.y, position.z);
        }

        private void UpdateMapPivot()
        {
            _map.Pivot = Vector3.Project(_player.Position, _moveDirection);
        }

        [Inject]
        public void Construct(Camera camera, IMap map, IPlayer player)
        {
            _camera = camera;
            _map = map;
            _player = player;
        }

        public void Initialize()
        {
            foreach (Vector3 direction in _map.Directions)
            {
                _moveDirection += direction;
            }

            _cameraDistance = Vector3.Project(_player.Position, _moveDirection) - Vector3.Project(_camera.transform.position, _moveDirection);
        }

        public void LateTick()
        {
            UpdateCameraPosition();
            UpdateMapPivot();
        }
    }
}
