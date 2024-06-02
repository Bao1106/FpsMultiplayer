using System;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

namespace Services
{
    [Serializable]
    public class Observer<T>
    {
        [SerializeField] private T value;
        [SerializeField] private UnityEvent<T> onValueChanged;

        public T Value
        {
            get => value;
            set => Set(value);
        }

        public Observer(T value, UnityAction<T> callback = null)
        {
            this.value = value;
            onValueChanged = new UnityEvent<T>();
            if(callback != null) onValueChanged.AddListener(callback);
        }
        
        public void Set(T newValue)
        {
            if(Equals(value, newValue)) return;
            value = newValue;
            Invoke();
        }

        public void Invoke()
        {
            Debug.Log($"Invoking: {onValueChanged.GetPersistentEventCount()} listeners");
            onValueChanged.Invoke(value);
        }

        public void AddListener(UnityAction<T> callback)
        {
            if (callback == null) return;
            if (onValueChanged == null) onValueChanged = new UnityEvent<T>();
            
            onValueChanged.AddListener(callback);
        }
        
        public void RemoveListener(UnityAction<T> callback)
        {
            if (callback == null) return;
            if (onValueChanged == null) return;
            
            onValueChanged.RemoveListener(callback);
        }
        
        public void RemoveAllListener()
        {
            if (onValueChanged == null) return;
            
            onValueChanged.RemoveAllListeners();
        }

        public void Dispose()
        {
            RemoveAllListener();
            onValueChanged = null;
            value = default;
        }
    }
}
