using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarAdapter : MonoBehaviour
{

    public Entity entity;
    public Microlight.MicroBar.MicroBar bar;

    // Start is called before the first frame update
    void Start()
    {
        BattleEventBus.getInstance().entityDamageEvent.AddListener(OnEntityDamage);

        bar.Initialize(entity.maxHP);
    }

    void OnEntityDamage(Entity e, float damage)
    {
        if(e == entity)
        {
            bar.UpdateHealthBar(e.hp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
