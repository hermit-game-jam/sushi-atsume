using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVScroll : MonoBehaviour
{
    public Vector2 speed;
    public string propertyName;

    private Material mat;
    private int propertyId;
    
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        propertyId = Shader.PropertyToID(propertyName);
    }
    
    void Update()
    {
        var current =  mat.GetTextureOffset(propertyId);
        var next = current + Time.deltaTime * speed;
        next = new Vector2(next.x % 1, next.y % 1);
        mat.SetTextureOffset(propertyId, next);
    }
}
