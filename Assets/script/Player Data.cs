using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharactersModels;

[System.Serializable]
public class PlayerData 
{
    public HealthModel healthModel;
    public Location location;
    public Money money;
    public Score score;
    public Mana mana;
}
