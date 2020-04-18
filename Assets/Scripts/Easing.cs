using UnityEngine;

public static class Easing {
    public static float Step(float t, float x) {
        if (t < x) return 0;
        return 1;
    }

    public static float Flyby(float t) {
        return t * (t * (4 * t - 6) + 3);
    }

    public static float CosInOut(float t) {
        return (1 - Mathf.Cos(t * Mathf.PI)) / 2;
    }

    public static float QuadIn(float t) {
        return 1 - QuadOut(1 - t);
    }

    public static float QuadOut(float t) {
        return t * t;
    }

    public static float QuadInOut(float t) {
        if (t < 0.5f) {
            return 2 * QuadOut(t);
        }
        return 1 - 2 * QuadOut(1 - t);
    }

    public static float CubicIn(float t) {
        return 1 - CubicOut(1- t);
    }

    public static float CubicOut(float t) {
        return t * t * t;
    }

    public static float CubicInOut(float t) {
        if (t < 0.5f) return 4 * t * t * t;
        return (t-1) * (2 * t - 2) * (2 * t - 2) + 1;
    }

    public static float QuintIn(float t) {
        return 1 - QuintOut(1- t);
    }

    public static float QuintOut(float t) {
        return t * t * t * t * t;
    }

    public static float ElasticOut(float t) {
        return 1 - Mathf.Pow(2, -8 * t) * Mathf.Cos(-3.5f * Mathf.PI * t);
    }
}