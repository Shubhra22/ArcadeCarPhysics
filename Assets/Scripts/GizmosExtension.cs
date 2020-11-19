using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosExtension:MonoBehaviour
{
    public static void DrawWireCircle(Vector3 pos, Quaternion rot, float radius, int detail = 32)
    {
        
        Vector3[] points3D = new Vector3[detail];

        for(int i = 0;i<detail; i++)
        {

            float angle = Mathf.PI * 2 * i / detail;

            float x = radius * (float) Math.Cos(angle);
            float y = radius * (float) Math.Sin(angle);
            
            Vector2 point2D = new Vector2(x, y);
            
            points3D[i] = pos + rot * point2D;

        }

        for (int i = 0; i < detail-1; i++)
        {
            Gizmos.DrawLine(points3D[i], points3D[i + 1]);
        }
        Gizmos.DrawLine(points3D[detail-1], points3D[0]);
    }
}
