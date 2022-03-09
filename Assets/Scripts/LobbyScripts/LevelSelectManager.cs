using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    Movement greg;

    private void Start(){
        greg = GameObject.FindWithTag("Player").GetComponent<Movement>();
    }

    private void Update(){
        if(greg.isLevelSelectOn){
            panel.SetActive(true);
        }else panel.SetActive(false);
    }

    public void ClosePanel(){
        panel.SetActive(false);
        greg.isLevelSelectOn = false;
    }

    public void Button1(){
        SceneManager.LoadScene(1);
    }
}
