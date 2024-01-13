using UnityEngine;

namespace yuki
{
    public interface IDragable
    {
        void Draged(Drag drag, Transform target);
    }
}
