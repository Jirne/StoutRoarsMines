using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadHealth : MonoBehaviour
{
    int segments = 50;

    public Slider slider;
    public Material material;

    

    Texture2D blackTexture;
    Texture2D whiteTexture;
    void Awake(){
        blackTexture = new Texture2D(128, 128);
        for (int y = 0; y < blackTexture.height; y++) {
            for (int x = 0; x < blackTexture.width; x++) {
                Color color = Color.black;
                blackTexture.SetPixel(x, y, color);
            }
        }
        Debug.Log("coucou");
    }

    public void UpdateHealth(int playerHealth, int playerMaxHealth, int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = playerMaxHealth;

        //On delete les anciennes vies        
        for(int i = 0; i < gameObject.transform.childCount; i++){
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if(child.name.Contains("LIFE_")){
                Destroy(child);
            }
        }


        //On en rajoute d'autres
        for (int i = 0; i < playerMaxHealth; i++){
            GameObject go = new GameObject("LIFE_"+i.ToString(), typeof(MeshFilter), typeof(MeshRenderer), typeof(RectTransform));
            go.transform.SetParent(gameObject.transform);
            go.GetComponent<MeshFilter>().mesh = DrawCircle(0, 0, 30, 0, 2 * Mathf.PI);
            go.GetComponent<MeshRenderer>().material = material;
            go.GetComponent<MeshRenderer>().material.renderQueue = 3010+i;
            go.GetComponent<MeshRenderer>().material.mainTexture = blackTexture;
            blackTexture.Apply();
            go.GetComponent<RectTransform>().anchorMin = new Vector2 (0, 1/2);
            go.GetComponent<RectTransform>().anchorMax = new Vector2 (0, 1/2);
            go.GetComponent<RectTransform>().localScale = new Vector2 (1, 1);

            float posX = 45+i*gameObject.GetComponent<RectTransform>().rect.width/slider.maxValue;
            float posY = gameObject.GetComponent<RectTransform>().rect.height/2;
            go.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(posX,posY,0);

            if(i < playerHealth){
                GameObject goLife = new GameObject("FULLLIFE_"+i.ToString(), typeof(MeshFilter), typeof(MeshRenderer), typeof(RectTransform));
                goLife.transform.SetParent(go.transform);
                goLife.GetComponent<MeshFilter>().mesh = DrawCircle(0, 0, 26, 0, 2 * Mathf.PI);
                goLife.GetComponent<MeshRenderer>().material = material;
                goLife.GetComponent<MeshRenderer>().material.renderQueue = 3050+i;
                goLife.GetComponent<RectTransform>().localScale = new Vector2 (1, 1);
                goLife.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0,0,0);
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
