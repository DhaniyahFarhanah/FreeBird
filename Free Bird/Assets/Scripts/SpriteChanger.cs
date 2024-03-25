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
    [SerializeField] Sprite up_normal;
    [SerializeField] Sprite left_normal;
    [SerializeField] Sprite down_normal;
    [SerializeField] Sprite right_normal;

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
        if(GameStateManager.GetControls()) //true means arrow
        {
            switch (combo[index])
            {
                case 'W':
                    imageHolder.sprite = up_normal;
                    break;
                case 'A':
                    imageHolder.sprite = left_normal;
                    break;
                case 'S':
                    imageHolder.sprite = down_normal;
                    break;
                case 'D':
                    imageHolder.sprite = right_normal;
                    break;

            }
        }
        else if (!GameStateManager.GetControls())
        {
            switch (combo[index])
            {
                case 'W':
                    imageHolder.sprite = w_normal;
                    break;
                case 'A':
                    imageHolder.sprite = a_normal;
                    break;
                case 'S':
                    imageHolder.sprite = s_normal;
                    break;
                case 'D':
                    imageHolder.sprite = d_normal;
                    break;

            }
        }

    
        
    }
}
