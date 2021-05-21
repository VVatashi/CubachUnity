﻿using UnityEngine;

public static class Interpolation
{
    public static float Lerp(float a, float b, float t)
    {
        return t * (b - a) + a;
    }

    public static Vector2 Lerp2(Vector2 a, Vector2 b, float t)
    {
        return t * (b - a) + a;
    }

    public static Vector3 Lerp3(Vector3 a, Vector3 b, float t)
    {
        return t * (b - a) + a;
    }

    public static float BiLerp(float a, float b, float c, float d, Vector2 t)
    {
        float l1 = Lerp(a, b, t.x);
        float l2 = Lerp(c, d, t.x);

        return Lerp(l1, l2, t.y);
    }

    public static float TriLerp(float a1, float b1, float c1, float d1, float a2, float b2, float c2, float d2, Vector3 t)
    {
        float bl1 = BiLerp(a1, b1, c1, d1, new Vector2(t.x, t.y));
        float bl2 = BiLerp(a2, b2, c2, d2, new Vector2(t.x, t.y));

        return Lerp(bl1, bl2, t.z);
    }
}
