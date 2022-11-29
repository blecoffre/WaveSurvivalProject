using System;
using UnityEngine;

namespace NeoFPS
{
    [HelpURL("https://docs.neofps.com/manual/neofpsref-mb-floatingorigintransform.html")]
    public class FloatingOriginTransform : FloatingOriginSubscriberBase
    {
        [SerializeField, Tooltip("When should the component subscribe and unsubscribe from the floating origin system. ObjectLifecycle means that the component will subscribe on Awake and unsubscribe when destroyed - the object will be repositioned when inactive. EnabledOnly means the component will subscribe when enabled and unsubscribe when disabled.")]
        private FloatingOrigin.SubscriptionPeriod m_SubscriptionPeriod = FloatingOrigin.SubscriptionPeriod.EnabledOnly;

        protected override FloatingOrigin.SubscriptionPeriod subscriptionPeriod
        {
            get { return m_SubscriptionPeriod; }
        }

        public override void ApplyOffset(Vector3 offset)
        {
            transform.position += offset;
        }
    }
}
