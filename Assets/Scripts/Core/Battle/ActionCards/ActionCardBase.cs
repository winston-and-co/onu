using UnityEngine;

namespace ActionCards
{
    public abstract class ActionCardBase : MonoBehaviour
    {
        public abstract string Name { get; }
        public int PlayerDataIndex { get; set; } = -1;
    }
}