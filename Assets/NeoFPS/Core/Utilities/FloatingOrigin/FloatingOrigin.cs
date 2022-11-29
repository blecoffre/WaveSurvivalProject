using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace NeoFPS
{
    [HelpURL("https://docs.neofps.com/manual/neofpsref-mb-floatingorigin.html")]
    public class FloatingOrigin : MonoBehaviour
    {
        [SerializeField, Tooltip("The distance from 0 on each axis before the floating origin system repositions object back towards the center.")]
        private float m_Threshold = 250f;

        [SerializeField, Tooltip("The transform to track the position of. This can be set at runtime via the API (such as when a character is spawned.")]
        private Transform m_Focus = null;

        public static FloatingOrigin system
        {
            get;
            private set;
        }

        private HashSet<IFloatingOriginSubscriber> m_Subscribers = new HashSet<IFloatingOriginSubscriber>();
        private Vector3 m_CurrentOffset = Vector3.zero;

        public enum SubscriptionPeriod
        {
            ObjectLifecycle,
            EnabledOnly
        }

        public static bool floatingOriginActive
        {
            get { return system != null && system.m_Focus != null; }
        }

        public float threshold
        {
            get { return m_Threshold; }
            set
            {
                m_Threshold = value;

                if (m_Focus != null)
                    CheckFocusPosition();
            }
        }

        public Transform currentFocus
        {
            get { return m_Focus; }
            set
            {
                m_Focus = value;

                if (m_Focus != null)
                    CheckFocusPosition();
            }
        }

        public Vector3 currentOffset
        {
            get { return m_CurrentOffset; }
        }

        public void AddSubscriber(IFloatingOriginSubscriber subscriber)
        {
            m_Subscribers.Add(subscriber);
        }

        public void RemoveSubscriber(IFloatingOriginSubscriber subscriber)
        {
            m_Subscribers.Remove(subscriber);
        }

        public static void SetFocus (Transform focus)
        {
            if (system != null)
                system.currentFocus = focus;
        }

        public static void SetThreshold(float threshold)
        {
            if (system != null)
                system.threshold = threshold;
        }

        void ApplyOffset(Vector3 offset)
        {
            m_CurrentOffset += offset;

            foreach(var sub in m_Subscribers)
                sub.ApplyOffset(offset);
        }

        private void Awake()
        {
            if (system != null)
            {
                Debug.LogError("Attempting to use multiple FloatingOriginManager components at the same time. You should have either 1 or 0 at any time.");
                Destroy(gameObject);
            }
            else
            {
                system = this;
            }
        }

        private void Start()
        {
            if (m_Focus != null && system == this)
                CheckFocusPosition();
        }

        private void OnDestroy()
        {
            if (system == this)
                system = null;
        }

        private void FixedUpdate()
        {
            if (m_Focus != null)
                CheckFocusPosition();
        }

        void CheckFocusPosition()
        {
            // Get the focus position
            Vector3 pos = m_Focus.position;

            // Scale down to threshold increments
            pos *= 1f / threshold;

            // Get number of steps on each axis (conversion to int truncates denominator)
            Vector3Int steps = new Vector3Int((int)pos.x, (int)pos.y, (int)pos.z);

            // Apply offset if non-zero
            if (steps != Vector3Int.zero)
            {
                Vector3 offset = steps;
                offset *= -threshold;
                ApplyOffset(offset);
            }
        }
    }
}
