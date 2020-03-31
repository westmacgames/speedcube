using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdRewardManager : MonoBehaviour
{    
    //True if the player has watched an ad to continue the game.
    public bool usedContinue;

    public GameManager gm;

    //Should get this from the gamemanager.
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (usedContinue)
        {
            usedContinue = false;
        }

    }
}
