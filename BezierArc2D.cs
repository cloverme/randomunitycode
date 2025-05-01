using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BezierArc2D : MonoBehaviour
{
    public Vector2 pointA = new Vector2(-5, -3);  // Start (bottom left)
    public Vector2 pointB = new Vector2(5, 3);    // End (top right)
    public float arcHeight = 5f;                  // Height at midpoint
    public int resolution = 50;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = resolution + 1;
        lineRenderer.useWorldSpace = true;

        DrawParabola();
    }

    void DrawParabola()
    {
        for (int i = 0; i <= resolution; i++)
        {
            float t = i / (float)resolution;
            Vector2 point = CalculateParabola(pointA, pointB, arcHeight, t);
            lineRenderer.SetPosition(i, new Vector3(point.x, point.y, 0));
        }
    }

    Vector2 CalculateParabola(Vector2 start, Vector2 end, float height, float t)
    {
        // Linear interpolation
        Vector2 mid = Vector2.Lerp(start, end, t);

        // Add parabolic offset
        float parabola = 4 * height * t * (1 - t); // peak at t=0.5
        return new Vector2(mid.x, mid.y + parabola);
    }
}
