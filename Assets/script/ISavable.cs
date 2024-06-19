using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISavable{
    public string GetID();
    public object ToData();
    public void FromData(object data);
}


