using UnityEngine;

public class ActionCardPouch : MonoBehaviour
{
    [SerializeField] ActionCardPicker picker;

    bool isOpen = false;

    void OnMouseDown()
    {
        if (isOpen)
            picker.Hide();
        else
            picker.Show();
        isOpen = !isOpen;
    }
}