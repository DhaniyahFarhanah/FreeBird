using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChanger : MonoBehaviour
{
    [SerializeField] int index;

    public GameObject image;
    [SerializeField] Sprite w_normal;
    [SerializeField] Sprite a_normal;
    [SerializeField] Sprite s_normal;
    [SerializeField] Sprite d_normal;

    Image imageHolder;

    string combo = "";

    // Start is called before the first frame update
    void Start()
    {
        imageHolder = image.GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        if (combo != GameStateManager.GetCombo() && GameStateManager.GetCombo().Length > 0)
        {
            combo = GameStateManager.GetCombo();
            SetSprite();
        }
        
    }

    void SetSprite()
    {
        imageHolder.color = Color.white;
        switch (combo[index])
        {
            case 'W': imageHolder.sprite = w_normal;
                break;
            case 'A': imageHolder.sprite = a_normal;
                break;
            case 'S': imageHolder.sprite = s_normal;
                break;
            case 'D': imageHolder.sprite = d_normal;
                break;

        }
    }
}
