using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private List<Card> m_Cards;
    public GameObject card_prefab;
    public Entity e;

	Color[] colors = new Color[] {
		Color.red,
		Color.blue,
		Color.green,
		Color.yellow,
	};

	public void Awake()
	{
		Generate();
		Shuffle();

        BattleEventBus.getInstance().cardDrawEvent.AddListener(OnConfirmDraw);
    }

    public void Generate()
    {
        for(int i = 0; i < 25; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, colors.Length);
            m_Cards.Add(
                Instantiate(card_prefab).GetComponent<Card>());
            m_Cards[i].color = colors[randomIndex];
            m_Cards[i].value = UnityEngine.Random.Range(0, 15);
            m_Cards[i].gameObject.SetActive(false);
            m_Cards[i].transform.SetParent(transform, false);
            m_Cards[i].entity = e;
        }
    }

	public Card Draw()
	{
		Card c = m_Cards[0];
		m_Cards.RemoveAt(0);
		return c;
	}

	Card Peek()
	{
		return m_Cards[0];
	}
    public void OnMouseDown()
    {
        BattleEventBus.getInstance().cardTryDrawEvent.Invoke(e, Peek());
    }

    public void OnConfirmDraw(Entity e, Card c)
    {
        if(e != this.e)
        {
            return;
        }
        Draw();
    }

	public void Shuffle()
	{
		System.Random rng = new System.Random();
		int n = m_Cards.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			Card value = m_Cards[k];
			m_Cards[k] = m_Cards[n];
			m_Cards[n] = value;
		}
	}

}