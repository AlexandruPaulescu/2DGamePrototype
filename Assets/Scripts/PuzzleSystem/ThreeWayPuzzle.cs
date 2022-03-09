using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreeWayPuzzle : MonoBehaviour
{
    public GameObject panel;
    
    public Button ak;                   //Ref to the first button
    public Button p90;                  //Ref to the second button
    public Button shotgun;              //Ref to the third button

    Movement greg;                      //We acces our Player's collision and we check if he collides with the GameObject that toggles the puzzle
    Sprite[] choices;                   //An array that contains multiple sprites

    void Start()
    {
        greg = GameObject.FindWithTag("Player").GetComponent<Movement>();       //Finding the Movement script   
        choices = Resources.LoadAll<Sprite>("guns");     //Assinging the sprites array all the sprites called "guns". IT HAS TO BE A MULTIPLE SPRITE
    }

    // Update is called once per frame
    void Update()
    {
        if(greg.isPuzzleOn){            //Just checking if the Player collided with the PuzzleTrigger
            panel.SetActive(true);
        }
        else panel.SetActive(false);
    }

    public void ClosePanel(){           //If we press the Close button we call this function that disables collision and deactivates the panel
        panel.SetActive(false);
        greg.isPuzzleOn = false;
    }

    private int nrOfPressesAK = 0;
    private bool isButton1;
    public void ChangeAK(){
        nrOfPressesAK++;
        Image myRend = ak.GetComponent<Image>();
        if(nrOfPressesAK == 1){
            myRend.sprite = choices[16];
        }
        else if(nrOfPressesAK == 2){
            myRend.sprite = choices[23];
        }
        else if(nrOfPressesAK == 3){
            myRend.sprite = choices[25];
        }

        if(nrOfPressesAK == 3)
            nrOfPressesAK = 0;
        
        if(myRend.sprite == choices[25])
            isButton1 = true;
    }

    private int nrOfPressesP90 = 0;
    private bool isButton2;
    public void ChangeP90(){
        nrOfPressesP90++;
        Image myRend = p90.GetComponent<Image>();
        if(nrOfPressesP90 == 1){
            myRend.sprite = choices[23];
        }
        else if(nrOfPressesP90 == 2){
            myRend.sprite = choices[16];
        }
        else if(nrOfPressesP90 == 3){
            myRend.sprite = choices[25];
        }
        if(nrOfPressesP90 == 3)
            nrOfPressesP90 = 0;

        if(myRend.sprite == choices[16])
            isButton2 = true;
    }

    private int nrOfPressesShotgun = 0;
    private bool isButton3;
    public void ChangeShohtgun(){
        nrOfPressesShotgun++;
        Image myRend = shotgun.GetComponent<Image>();
        if(nrOfPressesShotgun == 1){
            myRend.sprite = choices[16];
        }
        else if(nrOfPressesShotgun == 2){
            myRend.sprite = choices[25];
        }
        else if(nrOfPressesShotgun == 3){
            myRend.sprite = choices[23];
        }
        if(nrOfPressesShotgun == 3)
            nrOfPressesShotgun = 0;

        if(myRend.sprite == choices[23])
            isButton3 = true;
    }

    public void Finish(){
        if(isButton1 && isButton2 && isButton3){
            panel.SetActive(false);
            greg.isPuzzleOn = false;
            Destroy(panel);
            Debug.Log("cool");
        }
    }
}
