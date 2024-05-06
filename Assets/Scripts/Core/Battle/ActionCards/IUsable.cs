using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCards
{
    public interface IUsable
    {
        public string Name { get; }

        public void TryUse();
        public bool IsUsable();

        public void Use();
    }
}