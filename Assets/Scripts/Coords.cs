using UnityEngine;

public static class Coords
{
    public static Vector2 PolarToCartesian(float radius, float angle)
    {
        float x = radius * Mathf.Sin(angle);
        float y = radius * Mathf.Cos(angle);

        return new Vector2(x, y);
    }

    public static Vector3 SphericalToCartesian(float radius, float angleV, float angleH)
    {
        float x = radius * Mathf.Sin(angleV) * Mathf.Cos(angleH);
        float y = radius * Mathf.Sin(angleV) * Mathf.Sin(angleH);
        float z = radius * Mathf.Cos(angleV);

        return new Vector3(x, y, z);
    }
}
