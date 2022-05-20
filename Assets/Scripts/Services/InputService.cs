using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JM.TestTask
{
    public class InputService : MonoBehaviour, IPointerClickHandler
    {
        public event Action Click;

        public void OnPointerClick(PointerEventData eventData)
        {
            Click?.Invoke();
        }
    }
}
