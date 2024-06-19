using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSystem : MonoBehaviour
{
    public void Pause(){
        Time.timeScale = 0;
    }

    public void Continue(){
        Time.timeScale = 1;
    }

    public void BackToHome(){
        SceneManager.LoadScene(0);
    }
}
