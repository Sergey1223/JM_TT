using System;

namespace JM.TestTask
{
    public interface IBonusItem
    {
        event Action<IBonusItem> Activated;

        void Destroy();
    }
}
