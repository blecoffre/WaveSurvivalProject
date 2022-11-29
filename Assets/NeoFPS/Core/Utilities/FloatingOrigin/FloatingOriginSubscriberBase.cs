using System;
using UnityEngine;

namespace NeoFPS
{
    public abstract class FloatingOriginSubscriberBase : MonoBehaviour, IFloatingOriginSubscriber
    {
        private void Awake()
        {
            if (subscriptionPeriod == FloatingOrigin.SubscriptionPeriod.ObjectLifecycle)
                RegisterSubscriber(true);
        }

        private void OnDestroy()
        {
            if (subscriptionPeriod == FloatingOrigin.SubscriptionPeriod.ObjectLifecycle)
                UnregisterSubscriber();
        }

        private void OnEnable()
        {
            if (subscriptionPeriod == FloatingOrigin.SubscriptionPeriod.EnabledOnly)
                RegisterSubscriber(false);
        }

        private void OnDisable()
        {
            if (subscriptionPeriod == FloatingOrigin.SubscriptionPeriod.EnabledOnly)
                UnregisterSubscriber();
        }

        void RegisterSubscriber(bool applyOffset)
        {
            if (FloatingOrigin.system != null)
            {
                FloatingOrigin.system.AddSubscriber(this);
                if (applyOffset)
                    OnOffsetChanged(FloatingOrigin.system.currentOffset);
            }
        }

        void UnregisterSubscriber()
        {
            if (FloatingOrigin.system != null)
            {
                FloatingOrigin.system.RemoveSubscriber(this);
            }
        }

        private void OnOffsetChanged(Vector3 offset)
        {
            if (offset != Vector3.zero)
                transform.position += offset;
        }

        protected abstract FloatingOrigin.SubscriptionPeriod subscriptionPeriod { get; }

        public abstract void ApplyOffset(Vector3 offset);
    }
}