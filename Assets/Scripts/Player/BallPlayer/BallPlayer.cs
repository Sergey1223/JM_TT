using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace JM.TestTask
{
    public class BallPlayer : MonoBehaviour, IPlayer
    {
        private BallPlayerSettings _settings;
        private IMap _map;
        private InputService _inputService;
        private State _state;
        private int _currentDirectionPointer;
        private int _collidedTiles;

        public Vector3 Position => transform.position;

        public event Action TileActivated;
        public event Action BonusCollected;
        public event Action LeftMap;
        public event Action Died;

        private void Start()
        {
            _inputService.Click += OnInput;
            
            _state = State.Idle;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_state == State.Moving || _state == State.Idle)
            {
                if (other.gameObject.transform.TryGetComponent(_map.TileType, out _))
                {
                    _collidedTiles++;

                    TileActivated?.Invoke();
                }

                if (other.gameObject.transform.TryGetComponent<IBonusItem>(out _))
                {
                    BonusCollected?.Invoke();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_state == State.Moving)
            {
                if (other.gameObject.transform.TryGetComponent(_map.TileType, out _))
                {
                    _collidedTiles--;

                    if (_collidedTiles < 1)
                    {
                        Fall();
                    }
                }
            }
        }

        private void OnInput()
        {
            switch (_state)
            {
                case State.Idle:
                    StartMoving();

                    break;
                case State.Moving:
                    SwitchDirection();

                    break;
                case State.Fall:

                    break;
            }
        }

        private IEnumerator MovingCoroutine()
        {
            while (_state == State.Moving)
            {
                transform.position += _settings.Speed * Time.deltaTime * _map.Directions[_currentDirectionPointer];

                yield return null;
            }

            yield break;
        }

        private IEnumerator FallCoroutine()
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();

            rigidbody.isKinematic = false;
            rigidbody.AddForce(_map.Directions[_currentDirectionPointer] * _settings.Speed, ForceMode.Impulse);

            yield return new WaitForSeconds(_settings.FallTime);

            rigidbody.isKinematic = true;

            Died?.Invoke();
        }

        [Inject]
        public void Construct(BallPlayerSettings settings, IMap map, InputService inputService)
        {
            _settings = settings;
            _map = map;
            _inputService = inputService;
        }

        public void Fall()
        {
            _state = State.Fall;

            StartCoroutine(FallCoroutine());
        }

        public void ResetPosition()
        {
            throw new NotImplementedException();
        }

        public void Respawn()
        {
            _state = State.Idle;

            transform.position = _map.PlayerSpawnPosition;
        }

        public void StartMoving()
        {
            _state = State.Moving;

            StartCoroutine(MovingCoroutine());
        }

        public void SwitchDirection()
        {
            _currentDirectionPointer++;

            if (_currentDirectionPointer + 1 > _map.Directions.Length)
            {
                _currentDirectionPointer = 0;
            }
        }
    }
}
