using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Extension
{
	public static Vector2 GetRotatefdVector2(this Vector2 vector, float degree)
	{
        float sin = Mathf.Sin(degree * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degree * Mathf.Deg2Rad);

        return new Vector2(vector.x * cos - vector.y * sin,
                            vector.y * cos + vector.x * sin);
    }

    public static float GetVectorAngle(this Vector2 vectorA)
    {
        return Vector2.Angle(Vector2.up, vectorA);
    }
}
