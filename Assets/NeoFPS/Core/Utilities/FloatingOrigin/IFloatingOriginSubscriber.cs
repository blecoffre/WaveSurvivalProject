using System;
using UnityEngine;

namespace NeoFPS
{
    public interface IFloatingOriginSubscriber
    {
        void ApplyOffset(Vector3 offset);
    }
}
