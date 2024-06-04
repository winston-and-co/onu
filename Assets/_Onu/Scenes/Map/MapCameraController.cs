using UnityEngine;
using UnityEngine.EventSystems;

public class MapCameraController : MonoBehaviour, IScrollHandler
{
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    [SerializeField] public Vector2 panLimitMin;
    [SerializeField] public Vector2 panLimitMax;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    public void OnScroll(PointerEventData eventData)
    {
        Vector3 pos = cam.transform.position;
        pos.y += eventData.scrollDelta.y * panSpeed * Time.deltaTime * 10;

        // Clamp the position within the defined limits
        pos.x = Mathf.Clamp(pos.x, panLimitMin.x, panLimitMax.x);
        pos.y = Mathf.Clamp(pos.y, panLimitMin.y, panLimitMax.y);

        cam.transform.position = pos;
    }

    // void OnGUI()
    // {
    //     print("b");
    //     Vector3 pos = transform.position;

    //     if (Input.GetKey(KeyCode.W))
    //     {
    //         pos.y += panSpeed * Time.deltaTime;
    //     }
    //     if (Input.GetKey(KeyCode.S))
    //     {
    //         pos.y -= panSpeed * Time.deltaTime;
    //     }
    //     if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness)
    //     {
    //         pos.x += panSpeed * Time.deltaTime;
    //     }
    //     if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
    //     {
    //         pos.x -= panSpeed * Time.deltaTime;
    //     }

    //     // Clamp the position within the defined limits
    //     pos.x = Mathf.Clamp(pos.x, panLimitMin.x, panLimitMax.x);
    //     pos.y = Mathf.Clamp(pos.y, panLimitMin.y, panLimitMax.y);

    //     transform.position = pos;
    // }
}
