using UnityEngine;
using System.Collections;

public static class AIMovePatterns {

    private const float PI2 = Mathf.PI * 2;

    public static Vector3 FigureEight(ref float phase, ref bool invert, float radius, float speed)
    {
        if (phase >= PI2)
        {
            invert = !invert;
            phase -= PI2;
        }
        if(phase < 0) phase += PI2;
        Vector3 pos = Vector3.zero;
        pos.x = Mathf.Sin(phase) * (invert ? -1 : 1) * radius * Time.deltaTime;
        pos.y = Mathf.Cos(phase) * radius * Time.deltaTime;
        phase += speed * Time.deltaTime;
        return pos;
    }
}
