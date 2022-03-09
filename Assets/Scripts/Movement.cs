using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private Joystick js;                    //Ref to the Josystick
    [SerializeField]
    private Animator ar;                    //Ref to the animator for controlling animations

    [SerializeField]
    private GameObject gun;
    [SerializeField]
    private GameObject sword;

    [HideInInspector]
    public float hor;                       //The value of the Horizontal value of the josystick
    [HideInInspector]
    public float ver;                       //The value of the Vertical value of the josystick
    [HideInInspector]
    public bool isEntryLevel = false;       //Checking to see if the player has entered the level ( With a trigger )

    private float sens;                     //The sens of the Joystick ( basically the speed of the player )

    private Scene currentScene;
    private int buildIndex;
    private Rigidbody2D playerRB;

    #endregion

    #region MainScript
    void Awake(){
        currentScene = SceneManager.GetActiveScene ();
        buildIndex = currentScene.buildIndex; 
    }

    void FixedUpdate(){
        if(buildIndex!=0){                  //If you are NOT in Lobby
            if(gun.activeSelf){
            sens = 6f;
            }
            else if(sword.activeSelf){
                sens = 10f;
            }
            else{
                sens = 8f;
            }
        }else sens = 10f;                  //If you are in Lobby
        hor = js.Horizontal;
        ver = js.Vertical;
        #region Movement
        if (hor > 0)            //WALK RIGHT    
        {
            Vector3 rightMove = new Vector3(hor * sens * Time.fixedDeltaTime, 0.0f, 0.0f);
            transform.position += rightMove;
            ar.SetFloat("Horizontal", hor);
            ar.SetBool("isIdle", false);
        }
        if(hor < 0)        //WALK LEFT
        {
            Vector3 leftMove = new Vector3(hor * sens * Time.fixedDeltaTime, 0.0f, 0.0f);
            transform.position += leftMove;
            ar.SetFloat("Horizontal", hor);
            ar.SetBool("isIdle", false);
        }
        if(hor == 0)            //MAKING SURE IT GOES BACK TO "IDLE"
        {
            ar.SetBool("isIdle", true);
        }
        if(ver > 0)        //WALK UP
        {
            Vector3 upMove = new Vector3(0.0f, ver * sens * Time.fixedDeltaTime, 0.0f);
            transform.position += upMove;
            ar.SetFloat("Vertical", ver);
            ar.SetBool("isIdle", false);
        }
        if (ver < 0)       //WALK DOWN
        {
            Vector3 downMove = new Vector3(0.0f, ver * sens * Time.fixedDeltaTime, 0.0f);
            transform.position += downMove;
            ar.SetFloat("Vertical", ver);
            ar.SetBool("isIdle", false);
        }
        if (ver == 0)            //MAKING SURE IT GOES BACK TO "IDLE"
        {
            ar.SetBool("isIdle", true); 
        }
        #endregion
    }

    [HideInInspector]    public bool isPuzzleOn;
    [HideInInspector]    public bool isLevelSelectOn;
    void OnTriggerEnter2D(Collider2D hitInfo){
        if(hitInfo.gameObject.tag == "PuzzleTrigger"){
            isPuzzleOn = true;
        }
        if(hitInfo.gameObject.tag == "LevelSelectTrigger"){
            isLevelSelectOn = true;
        }
    }
    void OnTriggerExit2D(Collider2D hitInfo){
        if(hitInfo.gameObject.tag == "PuzzleTrigger"){
            isPuzzleOn = false;
        }
        if(hitInfo.gameObject.tag == "LevelSelectTrigger"){
            isLevelSelectOn = false;
        }
    }   

    #endregion

    #region Comments
    /*
    *
    *   Last Edit Date : 20/10/2020
    *   Purpose : Arranging the script + comments
    *   Name : Paulescu Alexandru Mohamad
    *
    *   GLIXEL LAB 2020
    */
    #endregion
}
