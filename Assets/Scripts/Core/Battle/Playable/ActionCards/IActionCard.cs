using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCards
{
    public interface IActionCard
    {
        public string Name { get; }

        public void TryUse();
        public bool IsUsable();

        public void Use();
    }
}