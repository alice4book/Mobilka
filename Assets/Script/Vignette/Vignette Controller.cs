using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VignetteController : MonoBehaviour
{
    public Shader shader;
    private Material _material;
    public float feathering;
    public float radius;
    public float minRadius;
    public Color tintColor;


    void Awake()
    {
        _material = new Material(shader);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        int width = source.width;
        int height = source.height;

        RenderTexture startRenderTexture = RenderTexture.GetTemporary(width, height);

        _material.SetFloat("_Radius", radius);
        _material.SetFloat("_Feathering", feathering);
        _material.SetColor("_TintColor", tintColor);
        Graphics.Blit(source, startRenderTexture, _material);
        Graphics.Blit(startRenderTexture, destination);

        RenderTexture.ReleaseTemporary(startRenderTexture);
    }
}
