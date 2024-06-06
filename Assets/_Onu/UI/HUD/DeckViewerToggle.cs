using UnityEngine;
using UnityEngine.EventSystems;

public class DeckViewerToggle : MonoBehaviour, IPointerClickHandler
{
    bool open = false;

    DeckViewer deckViewer;

    public void OnPointerClick(PointerEventData _)
    {
        if (open)
        {
            deckViewer.Hide();
            open = false;
        }
        else
        {
            if (deckViewer == null)
            {
                var go = PrefabLoader.GetInstance().InstantiatePrefab(PrefabType.UI_DeckViewer);
                go.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
                deckViewer = go.GetComponent<DeckViewer>();
            }
            deckViewer.Show();
            open = true;
        }
    }
}