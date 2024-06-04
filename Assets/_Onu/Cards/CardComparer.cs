using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class CardComparer : IComparer<AbstractCard>
    {
        readonly List<Color> colorOrder = new() {
        CardColor.Colorless,
        CardColor.Red,
        CardColor.Blue,
        CardColor.Green,
        CardColor.Yellow
    };

        public int Compare(AbstractCard x, AbstractCard y)
        {
            if (!colorOrder.Contains(x.Color) && !colorOrder.Contains(y.Color)) return 1;
            if (!colorOrder.Contains(x.Color)) return 1;
            if (!colorOrder.Contains(y.Color)) return -1;
            int res = colorOrder.IndexOf(x.Color).CompareTo(colorOrder.IndexOf(y.Color));
            if (res != 0) return res;
            // same color
            if (!x.Value.HasValue && !y.Value.HasValue) return 0;
            if (!x.Value.HasValue) return -1;
            if (!y.Value.HasValue) return 1;
            return ((int)x.Value).CompareTo((int)y.Value);
        }
    }
}