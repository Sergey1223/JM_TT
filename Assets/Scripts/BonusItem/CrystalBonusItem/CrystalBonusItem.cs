using System;
using System.Collections;
using UnityEngine;

namespace JM.TestTask
{
    public class CrystalBonusItem : MonoBehaviour, IBonusItem
    {
        public event Action<IBonusItem> Activated;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<MonoBehaviour>() is IPlayer)
            {
                Activated?.Invoke(this);

                Blow();
            }
        }

        private IEnumerator BlowCoroutine()
        {
            GetComponent<Renderer>().enabled = false;

            ParticleSystem particleSystem = GetComponent<ParticleSystem>();
            
            particleSystem.Play();

            yield return new WaitForSeconds(particleSystem.main.duration);

            Destroy();
        }

        private void Blow()
        {
            StartCoroutine(BlowCoroutine());
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
