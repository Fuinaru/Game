using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwirlScripts : MonoBehaviour
{

    [ExecuteInEditMode]

    public Vector2 radius = new Vector2(0.3f, 0.3f);

    public Vector2 center = new Vector2(0.5f, 0.5f);
    public float speed = 0;
    [Range(0.0f, 360.0f)]
    public static float angle = 360f;

    public Material material;
    private void Update()
    {
        if (angle < 360f) { GameManager.isTimePause = true; angle += Time.deltaTime * speed; }
        if (angle > 360f) { angle = 360f; GameManager.isTimePause = false; }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        Matrix4x4 rotationMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, angle), Vector3.one);

        material.SetMatrix("_RotationMatrix", rotationMatrix);
        material.SetVector("_CenterRadius", new Vector4(center.x, center.y, radius.x, radius.y));

        Graphics.Blit(source, destination, material);

    }
    
}