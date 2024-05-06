using System.Collections;
using UnityEngine;

namespace ActionCards
{
    public class WildCard : Playable
    {
        void Start()
        {
            Value = new NullableInt { Value = 0, IsNull = true };
            Color = CardColors.Colorless;
            entity = PlayerData.GetInstance().Player;
        }
    }
}