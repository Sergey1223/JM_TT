using UnityEngine;
using Zenject;

namespace JM.TestTask
{
    public interface IBonusItemFactory : IFactory<IBonusItem>
    {
        float ProbabilityDensity { get; }

        Vector3 RelativePosition { get; }
    }
}
