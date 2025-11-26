using UnityEngine;

/// <summary>
/// [Tile]
/// - Holds reference points for accurate tile length and alignment.
/// - startPoint: where the tile begins (front edge).
/// - endPoint: where the tile ends   (back edge).
/// </summary>
public class Tile : MonoBehaviour
{
    [Header("Tile End Points")]
    [Tooltip("Front/start point of this tile (Z-min or Z-max depending on design).")]
    public Transform startPoint;

    [Tooltip("Back/end point of this tile (opposite side from StartPoint).")]
    public Transform endPoint;

    /// <summary>
    /// Returns the absolute Z-length of this tile, based on startPoint and endPoint.
    /// </summary>
    public float GetLength()
    {
        if (startPoint == null || endPoint == null)
        {
            Debug.LogWarning($"{name}: StartPoint or EndPoint is not assigned. Using default length 10.");
            return 10f;
        }

        return Mathf.Abs(endPoint.position.z - startPoint.position.z);
    }

    /// <summary>
    /// Returns the world-space Z position of the start point.
    /// </summary>
    public float GetStartZ()
    {
        if (startPoint == null)
            return transform.position.z;

        return startPoint.position.z;
    }

    /// <summary>
    /// Returns the world-space Z position of the end point.
    /// </summary>
    public float GetEndZ()
    {
        if (endPoint == null)
            return transform.position.z;

        return endPoint.position.z;
    }
}
