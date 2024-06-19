using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class FilesHandler : MonoBehaviour
{
    public List<string> files = new ();

    public void GetAllSaveFiles(){
        string path = Application.persistentDataPath;

        if (Directory.Exists(path)){
            files = Directory.GetFiles(path)
            .Where(f => !string.IsNullOrEmpty(File.ReadAllText(f)))
            .Select(f => Path.GetFileName(f))
            .ToList();
        }
    }

    private void Awake() {
        GetAllSaveFiles();
    }
}
