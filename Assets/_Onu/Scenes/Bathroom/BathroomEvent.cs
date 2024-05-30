using UnityEngine;

public class BathroomEvent : MonoBehaviour
{
    public void Done()
    {
        FindObjectOfType<Bathroom>().eventDone.Invoke();
    }
}
