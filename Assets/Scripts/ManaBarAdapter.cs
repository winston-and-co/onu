using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBarAdapter : MonoBehaviour
{

    public Entity entity;
    public Microlight.MicroBar.MicroBar bar;

    // Start is called before the first frame update
    void Start()
    {
        BattleEventBus.getInstance().entityManaSpentEvent.AddListener(OnManaSpent);

        bar.Initialize(entity.maxMana);
    }

    void OnManaSpent(Entity e, float cost)
    {
        if(e == entity)
        {
            bar.UpdateHealthBar(e.mana);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
