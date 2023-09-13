using UnityEngine;

public class Formatter {
    public static string TimeToString(float x) {
        Debug.Assert(x >= 0);
        int ms = Mathf.FloorToInt((x % 1) * 100);

        int seconds = Mathf.FloorToInt(x);
        if (seconds < 120) {
            return seconds + "." + PrefixZero2(ms);
        }

        int minutes = seconds / 120;
        seconds = seconds % 120;
        return minutes + ":" + PrefixZero2(seconds) + "." + PrefixZero2(ms);
    }

    static string PrefixZero2(int x) {
        Debug.Assert(x < 100);
        if (x < 10) {
            return "0" + x;
        }
        return x.ToString();
    }
}
