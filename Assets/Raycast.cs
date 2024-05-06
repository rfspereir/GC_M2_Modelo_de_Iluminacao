using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    Ray ray;
    Texture2D tex;
    public float cameraSize;
    public Transform light1;
    [Range(0,30)]
    public float specular;
    [Range(0,1)]
    public float ambient;
    public bool autoUpdate;
    public Color ambientColor=Color.white;
    
    

    
    void Start()
    {
        ray= new Ray(transform.position, Vector3.forward);
        Renderer rend = GetComponent<Renderer>();
        tex= new Texture2D(100, 100);
        tex.filterMode = FilterMode.Trilinear;
        rend.material.mainTexture = tex;
    }

    void Update()
    {
        Debug.DrawRay(ray.origin, ray.direction* 5.0f, Color.red);
    }
    public void RenderButtonClicked()
    {
        ClearTexture(); // Limpa a textura
        StartCoroutine(RenderScene());
    }

    public void ClearTexture()
{
    Color[] clearPixels = new Color[tex.width * tex.height];
    for (int i = 0; i < clearPixels.Length; i++)
    {
        clearPixels[i] = Color.white;
    }
    tex.SetPixels(clearPixels);
    tex.Apply();
}
    public IEnumerator RenderScene(){
        for (int y = 0; y < tex.height; y++)
        {
            for (int x = 0; x < tex.width; x++)
            {
                float px = ((float)x/tex.width) * cameraSize-cameraSize*.5f;
                float py = ((float)y/tex.height) * cameraSize-cameraSize*.5f;
                ray.origin = new Vector3(px, py, 0);
                if (Physics.Raycast(ray, out RaycastHit hit)){
                    Color c = BlinnPhong(hit);
                    tex.SetPixel(x, y, c);
                }else{
                    tex.SetPixel(x, y, ambientColor);
                }
                 tex.Apply();
            }
        }
         yield return null;
       
    }

    Color BlinnPhong(RaycastHit hit){
        Color hitColor = hit.transform.GetComponent<MeshRenderer>().material.color;
        Vector3 L = (light1.position-hit.point).normalized;
        Vector3 N = hit.normal;
        Vector3 V = (transform.position-hit.point).normalized;
        Vector3 H = (L+V).normalized;
        float NdotH = Mathf.Max(0, Vector3.Dot(N,H));
        float diff = Mathf.Max(0, Vector3.Dot(N,L));
        float spec = Mathf.Pow(NdotH, (specular*2+1));
        return ambient*hitColor + diff*hitColor + spec*Color.white;
    }
}
