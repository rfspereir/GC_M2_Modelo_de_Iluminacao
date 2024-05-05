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
        tex= new Texture2D(50, 50);
        tex.filterMode = FilterMode.Trilinear;
        rend.material.mainTexture = tex;            
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            StartCoroutine("RenderScene");
        }
        Debug.DrawRay(ray.origin, ray.direction* 5.0f, Color.red);
        
    }

    IEnumerator RenderScene(){
        for (int y = 0; y < tex.height; y++)
        {
            for (int x = 0; x < tex.width; x++)
            {
                float px = ((float)x/tex.width) * cameraSize-cameraSize*.5f;
                float py = ((float)y/tex.height) * cameraSize-cameraSize*.5f;
                ray.origin = new Vector3(px, py, 0);
                if (Physics.Raycast(ray, out RaycastHit hit)){
                    Color c = hit.transform.GetComponent<MeshRenderer>().material.color;
                    tex.SetPixel(x, y, c);
                }else{
                    tex.SetPixel(x, y, Color.black);
                }
                 tex.Apply();
            }
        }
         yield return new WaitForSeconds(0.0001f);
       
    }
}
