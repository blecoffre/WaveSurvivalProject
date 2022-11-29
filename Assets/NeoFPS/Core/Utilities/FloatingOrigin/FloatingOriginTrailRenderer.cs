using System;
using UnityEngine;

namespace NeoFPS
{
    [HelpURL("https://docs.neofps.com/manual/neofpsref-mb-floatingorigintrailrenderer.html")]
    [RequireComponent(typeof (TrailRenderer))]
    public class FloatingOriginTrailRenderer : FloatingOriginSubscriberBase
    {
        private TrailRenderer m_TrailRenderer = null;

        private static Vector3[] s_Positions = null;

		protected override FloatingOrigin.SubscriptionPeriod subscriptionPeriod
		{
			get { return FloatingOrigin.SubscriptionPeriod.EnabledOnly; }
		}

		void Awake()
        {
			m_TrailRenderer = GetComponent<TrailRenderer>();
        }

        public override void ApplyOffset(Vector3 offset)
		{
			int pointCount = m_TrailRenderer.positionCount;
			if (pointCount > 0)
			{
				// Create or extend particle buffer if required
				if (s_Positions == null || pointCount > s_Positions.Length)
				{
					int targetCount = (pointCount / 64) + 1;
					s_Positions = new Vector3[targetCount * 64];
				}

				// Get the particles
				int count = m_TrailRenderer.GetPositions(s_Positions);

				// Offset the particles (skip the last one as that's current position)
				for (int i = 0; i < count - 1; ++i)
					s_Positions[i] += offset;

				// Set the particle system contents
				m_TrailRenderer.SetPositions(s_Positions);
			}
		}
    }
}
