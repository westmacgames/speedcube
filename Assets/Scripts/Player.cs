using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Stops all input if true.
    public bool blockInput;

    //Stops all forward movement if true.
    public bool blockMovement;

    //Movement positions for players.
    public Transform right;
    public Transform middle;
    public Transform left;

    //A number indicating player position. -1 Left. 0 Middle. 1 Right.
    public int positionIndex;

    public float movementSpeed = 35;

    //Used by smoothdamp.
    public float smoothSpeed = 0.1f;
    //Value used exclusively by smoothdamp.
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //When the game ends, the timescale is set to 0. This is done to make sure it doesnt stop running.
        Time.timeScale = 1;
    }

    //Control position index. The lane is based on this.
    //Control right movement.
    public void Right()
    {       
        if (!blockInput)
        {
            positionIndex++;
        }
    }

    //Control left movement.
    public void Left()
    {
        if (!blockInput)
        {
            positionIndex--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!blockMovement)
        {
            //Move the player forward.
            transform.Translate(transform.forward * -movementSpeed * Time.deltaTime);
        }

        //Grab touchcount
        int nTouch = Input.touchCount;

        for (int i = 0; i < nTouch; i++)
        {
            //Grab data from the touch
            Touch touch = Input.GetTouch(i);
            TouchPhase phase = touch.phase;

            if (phase == TouchPhase.Began)
            {
                //Check the half of the screen it is on
                if (touch.position.x > Screen.width / 2)
                {
                    Right();
                }
                else
                {
                    Left();
                }
            }
        }
        //Reset position index if it is out of bounds.
        if (positionIndex > 1)
        {
            positionIndex = 1;
        }
        if (positionIndex < -1)
        {
            positionIndex = -1;
        }

        //Set player x position based on index.

        //Move the player with a bit of a delay to the desired lane.
        //Left
        if (positionIndex == -1) { transform.position = Vector3.SmoothDamp(transform.position, 
            new Vector3(left.position.x, transform.position.y, transform.position.z), ref velocity, smoothSpeed); }

        //Right
        if (positionIndex == 1) { transform.position = Vector3.SmoothDamp(transform.position,
            new Vector3(right.position.x, transform.position.y, transform.position.z), ref velocity, smoothSpeed);
        }

        //Middle
        if (positionIndex == 0) { transform.position = Vector3.SmoothDamp(transform.position,
            new Vector3(middle.position.x, transform.position.y, transform.position.z), ref velocity, smoothSpeed);
        }

        //Go to the main menu with escape.
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

}