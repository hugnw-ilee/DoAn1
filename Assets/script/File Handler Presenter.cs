using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class FileHandlerPresenter : MonoBehaviour
{
    private FilesHandler filesHandler;
    public GameObject loadPanel;
    public GameObject button;


    private void Start() {
        filesHandler = GetComponent<FilesHandler>();
        LoadOnUI();
    }

    public void ButtonLoad(string name){
        GameObject uiButton = Instantiate(button);
        uiButton.transform.SetParent(loadPanel.transform,false);
        TMP_Text uiText = uiButton.GetComponentInChildren<TMP_Text>();
        if(uiText != null){
            uiText.text = name;
        }
    }


    public void LoadOnUI(){
        foreach(string filename in filesHandler.files){
            ButtonLoad(filename);
        }
    }

    public void DelOnUI(){

    }

}
