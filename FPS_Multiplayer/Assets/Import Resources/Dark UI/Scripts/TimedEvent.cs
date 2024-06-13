using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Michsky.UI.Dark
{
    public class TimedEvent : MonoBehaviour
    {
        [Header("TIMING (SECONDS)")]
        public float timer = 4;
        public bool enableAtStart;

        [Header("TIMER EVENT")]
        public UnityEvent timerAction;

        void Start()
        {
            if(enableAtStart == true)
            {
                StartCoroutine(nameof(TimedEventStart));
            }
        }

        IEnumerator TimedEventStart()
        {
            yield return new WaitForSeconds(timer);
            timerAction.Invoke();
        }

        public void StartIEnumerator ()
        {
            StartCoroutine(nameof(TimedEventStart));
        }

        public void StopIEnumerator ()
        {
            StopCoroutine(nameof(TimedEventStart));
        }
    }
}
