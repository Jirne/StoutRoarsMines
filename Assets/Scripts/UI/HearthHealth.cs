using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthHealth : MonoBehaviour
{
    // Start is called before the first frame update
    private int halfHearth;
    private int fullHearth;
    private int emptyHearth;

    public Sprite fullHearthSprite;
    public Sprite halfHearthSprite;
    public Sprite emptyHearthSprite;

    const int MAX_DELTA = 115;
    void Start()
    {
    }

    public void UpdateHealth(int playerHealth, int playerMaxHealth)
    {
        halfHearth = playerHealth%2;
        fullHearth = playerHealth/2;
        emptyHearth = playerMaxHealth/2 - fullHearth - halfHearth;

        for (int i = 0; i < gameObject.transform.childCount; i++)
            Destroy(gameObject.transform.GetChild(i).gameObject);

        for (int i = 1; i <= fullHearth; i++)
        {
            GameObject go = new GameObject("FULL_"+i.ToString());
            SpriteRenderer sprite = go.AddComponent<SpriteRenderer>();
            sprite.sprite = fullHearthSprite;
            go.transform.parent = gameObject.transform;
            RectTransform rT = go.AddComponent<RectTransform>();
            rT.anchorMax = new Vector2(0,1);
            rT.anchorMin = new Vector2(0,1);
            rT.localScale = new Vector2(100,100);
            rT.anchoredPosition3D = new Vector3(50 + (i - 1)*MAX_DELTA,-45,0);
        }

        if(halfHearth != 0){
            GameObject go = new GameObject("HALF");
            SpriteRenderer sprite = go.AddComponent<SpriteRenderer>();
            sprite.sprite = halfHearthSprite;
            go.transform.parent = gameObject.transform;
            RectTransform rT = go.AddComponent<RectTransform>();
            rT.anchorMax = new Vector2(0,1);
            rT.anchorMin = new Vector2(0,1);
            rT.localScale = new Vector2(100,100);
            rT.anchoredPosition3D = new Vector3(50 + (fullHearth)*MAX_DELTA,-45,0);
        }

        for (int i = 1; i <= emptyHearth; i++)
        {
            GameObject go = new GameObject("EMPTY_"+i.ToString());
            SpriteRenderer sprite = go.AddComponent<SpriteRenderer>();
            sprite.sprite = emptyHearthSprite;
            go.transform.parent = gameObject.transform;
            RectTransform rT = go.AddComponent<RectTransform>();
            rT.anchorMax = new Vector2(0,1);
            rT.anchorMin = new Vector2(0,1);
            rT.localScale = new Vector2(100,100);
            rT.anchoredPosition3D = new Vector3(50 + (fullHearth + halfHearth + i - 1)*MAX_DELTA,-45,0);
        }
    }
}
