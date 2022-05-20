using System;
using System.Collections;
using UnityEngine;

namespace JM.TestTask
{
    [Serializable]
    public class CubicTile : MonoBehaviour
    {
        private const float G = 9.8f; 

        [SerializeField]
        private float _fallDelay;

        [SerializeField]
        private float _fallTime; 

        public float FallDelay { get => _fallDelay; set => _fallDelay = value; }
        public float FallTime { get => _fallTime; set => _fallTime = value; }
        
        public event Action<CubicTile> Activated;
        public event Action<CubicTile> Destroyed;

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<MonoBehaviour>() is IPlayer)
            {
                Activated?.Invoke(this);

                PlayFallAnimation();
            }
        }

        private IEnumerator FallCoroutine()
        {
            yield return new WaitForSeconds(FallDelay);

            float time = 0;
            float startPosition = transform.position.y;

            while (time < FallTime)
            {
                time += Time.deltaTime;

                transform.position = new Vector3(transform.position.x, startPosition - G * time * time / 2, transform.position.z);

                yield return null;
            }

            Destroyed?.Invoke(this);

            yield break;
        }

        private void PlayFallAnimation()
        {
            StartCoroutine(FallCoroutine());
        }

        public void Reset()
        {
            StopAllCoroutines();
        }
    }
}
