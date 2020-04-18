using UnityEngine;

public class MathUtils {
    public static Vector2 PolarToCartesian(float angle, float length = 1) {
        return PolarToCartesianRad(angle * Mathf.Deg2Rad, length);
    }

    public static Vector2 PolarToCartesianRad(float angleRad, float length = 1) {
        return new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)) * length;
    }

    public static float VectorToAngle(Vector3 v) {
        return VectorToAngle((Vector2) v);
    }

    public static float VectorToAngle(Vector2 v) {
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    public static float WrapWithin(float x, float lower, float upper) {
        Debug.Assert(upper > lower);
        return Mathf.Repeat(x - upper, upper - lower) + lower;
    }

    public static float Map(float value, float fromLower, float fromUpper, float toLower, float toUpper) {
        Debug.Assert(fromLower < fromUpper);
        Debug.Assert(toLower < toUpper);

        float fromAbs = value - fromLower;
        float fromMaxAbs = fromUpper - fromLower;

        float normal = fromAbs / fromMaxAbs;

        float toMaxAbs = toUpper - toLower;
        float toAbs = toMaxAbs * normal;

        return toAbs + toLower;
    }
}