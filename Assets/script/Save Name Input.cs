using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SaveNameInput : MonoBehaviour
{

    public TMP_InputField inputField;
    private string fileName;

    public void Save(){
        fileName = inputField.text;
        SaveSystem.Instance.SaveData(fileName);
    }
}
