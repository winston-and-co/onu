
using UnityEngine;
using UnityEngine.UI;

public class DrawPath : MonoBehaviour
{
    public RectTransform startPoint; // Reference to the start point object
    public RectTransform endPoint; // Reference to the end point object
    public Image lineSegmentPrefab; // Reference to the dotted line segment prefab
    public float segmentLength = 10f; // Length of each line segment
    public float gapLength = 5f; // Length of each gap between line segments


    void Start()
    {
        Vector2 direction = endPoint.position - startPoint.position;
        float distance = direction.magnitude;
        Vector2 normalizedDirection = direction.normalized;
        int segmentCount = Mathf.CeilToInt(distance / (segmentLength + gapLength));
        Debug.Log("Distance" + distance + "segments" + segmentCount);
        float actualSegmentLength = distance / segmentCount;
        float actualGapLength = gapLength / (segmentCount - 1);
        for (int i = 0; i < segmentCount; i++)
        {
            Vector2 segmentPosition = (Vector2)startPoint.position + normalizedDirection * (actualSegmentLength * i + actualGapLength * i);
            Image lineSegment = Instantiate(lineSegmentPrefab, segmentPosition, Quaternion.identity, transform);
            lineSegment.transform.SetParent(transform);
            lineSegment.rectTransform.sizeDelta = new Vector2(actualSegmentLength, lineSegment.rectTransform.sizeDelta.y);
            lineSegment.rectTransform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        }
    }
}