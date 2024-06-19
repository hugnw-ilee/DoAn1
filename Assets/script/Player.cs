using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharactersModels;
using PlayerPresenter;

public class Player : MonoBehaviour, ISavable
{
    public HealthModel healthModel;
    public Location location;
    public Money money;
    public Score score;
    public Mana mana;
    public string GetID() => gameObject.name;


    public object ToData()
    {
        return new PlayerData
        {
            healthModel = healthModel,
            location = location,
            money = money,
            score = score,
            mana = mana,
        };
    }

    public void FromData(object data)
    {
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        if (data is PlayerData playerData)
        {
            healthModel.maxHealth = playerData.healthModel.maxHealth;
            healthModel.curHealth = playerData.healthModel.curHealth;
            playerHealth.GetHealth();
            playerData.location.GetLocation(transform);
            money = playerData.money;
            score = playerData.score;
            mana = playerData.mana;
        }
    }

}

