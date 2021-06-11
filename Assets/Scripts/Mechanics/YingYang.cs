using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class YingYang : MonoBehaviour
{
    int segments = 50;
    int radius = 100;
    RectTransform prt;
    public float playerHealth;
    public float playerMaxHealth;


    // Start is called before the first frame update
    void Awake() {
        prt = GetComponent<RectTransform>();
        //On crée la texture noire
        Texture2D blacktexture = new Texture2D(128, 128);
        for (int y = 0; y < blacktexture.height; y++) {
            for (int x = 0; x < blacktexture.width; x++) {
                Color color = Color.black;
                blacktexture.SetPixel(x, y, color);
            }
        }



        GetComponent<MeshFilter>().mesh = DrawCircle(0, 0, radius+2, 0, 2*Mathf.PI);
        //On modifie les gameObject en fonction de la taille du parent
        foreach (Transform eachChild in transform) {
            RectTransform rt = eachChild.GetComponent<RectTransform>();
            switch (eachChild.name) {
                case "BlankBlack":
                    eachChild.GetComponent<Renderer>().material.mainTexture = blacktexture;
                    blacktexture.Apply();
                    eachChild.GetComponent<MeshFilter>().mesh = DrawCircle(0, 0, radius, Mathf.PI / 2, 3 *Mathf.PI / 2);
                    eachChild.GetComponent<Renderer>().material.renderQueue = 3002;
                    break;
                case "HalfWhite":
                    eachChild.GetComponent<MeshFilter>().mesh = DrawCircle(0, 0, radius, -Mathf.PI/2, Mathf.PI/2);
                    eachChild.GetComponent<Renderer>().material.renderQueue = 3002;
                    break;

                case "CircleWhite":
                    eachChild.GetComponent<MeshFilter>().mesh = DrawCircle(0, 0, radius/2, 0, 2 * Mathf.PI);
                    rt.anchoredPosition = new Vector3(radius / 2 * Mathf.Cos(Mathf.PI / 2), radius / 2 * Mathf.Sin(Mathf.PI / 2));

                    eachChild.GetComponent<Renderer>().material.renderQueue = 3005;
                    break;

                case "CricleBlack":
                    eachChild.GetComponent<Renderer>().material.mainTexture = blacktexture;
                    blacktexture.Apply();
                    eachChild.GetComponent<MeshFilter>().mesh = DrawCircle(0, 0, radius/2, 0, 2 * Mathf.PI);
                    rt.anchoredPosition = new Vector3(radius / 2 * Mathf.Cos(-Mathf.PI / 2), radius / 2 * Mathf.Sin(- Mathf.PI / 2));


                    eachChild.GetComponent<Renderer>().material.renderQueue = 3005;
                    break;

                case "LittleWhite":
                    eachChild.GetComponent<MeshFilter>().mesh = DrawCircle(0, 0, radius/5, 0, 2 * Mathf.PI);
                    eachChild.GetComponent<Renderer>().material.renderQueue = 3010;
                    rt.anchoredPosition = new Vector3(radius / 2 * Mathf.Cos(-Mathf.PI / 2), radius / 2 * Mathf.Sin( -Mathf.PI / 2));
                    break;

                case "LittleBlack":
                    eachChild.GetComponent<Renderer>().material.mainTexture = blacktexture;
                    blacktexture.Apply();
                    eachChild.GetComponent<MeshFilter>().mesh = DrawCircle(0, 0, radius/5, 0, 2 * Mathf.PI);
                    eachChild.GetComponent<Renderer>().material.renderQueue = 3010;
                    rt.anchoredPosition = new Vector3(radius / 2 * Mathf.Cos(Mathf.PI / 2), radius / 2 * Mathf.Sin(Mathf.PI / 2));
                    break;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float midlife = playerMaxHealth / 2;
        float littleradius = radius / 2;
        float scale;
        float bWhiteAng = (float)(Mathf.PI * (2 * (playerHealth / playerMaxHealth) - 0.5));
        //float bWhiteAng = (float)(Mathf.PI * (-2 * (playerHealth / playerMaxHealth) - 0.5));
        float bBlackAng = (float)(Mathf.PI * (-2 * (playerHealth / playerMaxHealth) + 2.5));
        //float bBlackAng = (float)(Mathf.PI * (1 * (playerHealth / playerMaxHealth) - 1));
        foreach (Transform eachChild in transform) {
            RectTransform rt = eachChild.GetComponent<RectTransform>();
            switch (eachChild.name) {
                case "BlankBlack":
                    rt.sizeDelta = prt.sizeDelta;
                    if (playerHealth <= midlife) {
                        eachChild.GetComponent<MeshFilter>().mesh = DrawCircle(0, 0, radius, Mathf.PI / 2, bBlackAng);
                        eachChild.GetComponent<Renderer>().material.renderQueue = 3002;
                    }
                    else {
                        eachChild.GetComponent<MeshFilter>().mesh = DrawCircle(0, 0, radius, Mathf.PI / 2, 3 * Mathf.PI / 2);
                        eachChild.GetComponent<Renderer>().material.renderQueue = 3001;
                    }
                    break;
                case "HalfWhite":
                    rt.sizeDelta = prt.sizeDelta;
                    if (playerHealth >= midlife) {
                        eachChild.GetComponent<MeshFilter>().mesh = DrawCircle(0, 0, radius, -Mathf.PI / 2, bWhiteAng);
                        eachChild.GetComponent<Renderer>().material.renderQueue = 3002;
                    }
                    else {
                        eachChild.GetComponent<MeshFilter>().mesh = DrawCircle(0, 0, radius, -Mathf.PI / 2, Mathf.PI / 2);
                        eachChild.GetComponent<Renderer>().material.renderQueue = 3001;
                    }
                    break;

                case "CircleWhite":
                    rt.sizeDelta = prt.sizeDelta / 2;
                    if (playerHealth > midlife) {
                        rt.anchoredPosition = new Vector3(littleradius * Mathf.Cos(bWhiteAng), littleradius * Mathf.Sin(bWhiteAng));
                        eachChild.GetComponent<Renderer>().material.renderQueue = 3005;
                    }
                    else {
                        rt.anchoredPosition = new Vector3(radius / 2 * Mathf.Cos(Mathf.PI / 2), radius / 2 * Mathf.Sin(Mathf.PI / 2));
                        eachChild.GetComponent<Renderer>().material.renderQueue = 3004;
                    }
                    break;

                case "CricleBlack":
                    rt.sizeDelta = prt.sizeDelta / 2;
                    if (playerHealth < midlife) {
                        rt.anchoredPosition = new Vector3(littleradius * Mathf.Cos(bBlackAng), littleradius * Mathf.Sin(bBlackAng));
                        eachChild.GetComponent<Renderer>().material.renderQueue = 3005;
                    }
                    else {
                        rt.anchoredPosition = new Vector3(radius / 2 * Mathf.Cos(-Mathf.PI / 2), radius / 2 * Mathf.Sin(-Mathf.PI / 2));
                        eachChild.GetComponent<Renderer>().material.renderQueue = 3004;
                    }
                    break;

                case "LittleWhite":
                    rt.sizeDelta = prt.sizeDelta / 5;
                    if (playerHealth >= midlife) {
                        //On scale de 1 a 2.5
                        scale = (float)((3 * playerHealth / playerMaxHealth) - 0.5);
                        rt.localScale = new Vector3(scale, scale);
                        rt.anchoredPosition = new Vector3(radius / 2 * Mathf.Cos(-Mathf.PI / 2), radius / 2 * Mathf.Sin(-Mathf.PI / 2));
                    }
                    else {
                        //On scale de 0 a 1
                        scale = (float)((2 * playerHealth / playerMaxHealth));
                        rt.localScale = new Vector3(scale, scale);
                        rt.anchoredPosition = new Vector3(littleradius * Mathf.Cos(bBlackAng), littleradius * Mathf.Sin(bBlackAng));
                    }
                    break;

                case "LittleBlack":
                    rt.sizeDelta = prt.sizeDelta / 5;
                    if (playerHealth <= midlife) {
                        //On scale de 1 a 2.5
                        scale = (float)((-3 * playerHealth / playerMaxHealth) + 2.5);
                        rt.localScale = new Vector3(scale, scale);
                        rt.anchoredPosition = new Vector3(radius / 2 * Mathf.Cos(Mathf.PI / 2), radius / 2 * Mathf.Sin(Mathf.PI / 2));
                    }
                    else {
                        //On scale de 0 a 1
                        scale = (float)((-2 * playerHealth / playerMaxHealth) + 2);
                        rt.localScale = new Vector3(scale, scale);
                        rt.anchoredPosition = new Vector3(littleradius * Mathf.Cos(bWhiteAng), littleradius * Mathf.Sin(bWhiteAng));
                       }
                    break;
            }
        }

    }

    private Mesh DrawCircle(float xo , float yo, float r, float start, float finish) {
        Mesh mesh = new Mesh();
        List<Vector3> verticies = new List<Vector3>();
        List<int> triangles = new List<int>();

        verticies.Add(new Vector3(xo, yo));
        verticies.Add(new Vector3(xo + r * Mathf.Cos(start + 2 * 0 * Mathf.PI / segments), yo + r * Mathf.Sin(start + 2 * 0 * Mathf.PI / segments)));

        for (int i = 1; i <= segments; i++) {
            if (start + 2 * i * Mathf.PI / segments <= finish) { 
                verticies.Add(new Vector3(xo + r * Mathf.Cos(start + 2 * i * Mathf.PI / segments), yo + r * Mathf.Sin(start + 2 * i * Mathf.PI / segments)));
                triangles.Add(0);
                triangles.Add(i);
                triangles.Add(i + 1);
            }
        }

        mesh.vertices = verticies.ToArray();
        mesh.triangles = triangles.ToArray();
         
        return mesh;
    }
}
