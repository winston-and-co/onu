using UnityEngine;

public class FlushCards : MonoBehaviour
{
    [SerializeField] int num_lost = 2;

    public void Flush()
    {
        // TODO: Use card picker
        var p = PlayerData.GetInstance().Player;
        Deck d = p.deck;

        for (int i = 0; i < num_lost; i++)
        {
            d.RemovePermanently(0);
        }
        GetComponent<BathroomEvent>().Done();
    }

    public void DontFlush()
    {
        GetComponent<BathroomEvent>().Done();
    }
}
