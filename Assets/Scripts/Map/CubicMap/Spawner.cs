using System.Collections.Generic;
using UnityEngine;

namespace JM.TestTask
{
    public class Spawner
    {
        private Queue<MonoBehaviour> _buffer = new Queue<MonoBehaviour>();

        private protected MonoBehaviour Dequeue()
        {
            if (_buffer.Count > 0)
            {
                MonoBehaviour monoBehaviour = _buffer.Dequeue();

                monoBehaviour.gameObject.SetActive(true);

                return monoBehaviour;
            }

            return null;
        }

        private protected void Enqueue(MonoBehaviour monoBehaviour)
        {
            if (!_buffer.Contains(monoBehaviour))
            {
                monoBehaviour.gameObject.SetActive(false);

                _buffer.Enqueue(monoBehaviour);
            }
        }
    }
}
