using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    Ray ray;
    Texture2D tex;
    public float cameraSize;

    
    void Start()
    {
        ray= new Ray(transform.position, Vector3.forward);
        Renderer rend = GetComponent<Renderer>();
        tex= new Texture2D(256, 256);
        tex.filterMode = FilterMode.Point;
        rend.material.mainTexture = tex;            
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            StartCoroutine("RenderScene");
        }
        Debug.DrawRay(ray.origin, ray.direction* 5.0f, Color.red);
        
    }
}
