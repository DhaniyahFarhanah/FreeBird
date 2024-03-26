using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgEffectScript : MonoBehaviour
{

    [SerializeField] float speedMove;
    [SerializeField] Vector2 moveTowardsPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.GetGameStatus() && !GameStateManager.GetEnd() && !GameStateManager.GetCutscene()) //Playing the game
        {
            transform.position = Vector2.MoveTowards(transform.position, moveTowardsPoint, speedMove * Time.deltaTime);
        }
            
    }
}
