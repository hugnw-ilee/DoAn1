using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharactersModels;

public class Enemy : MonoBehaviour, ISavable{
    public HealthModel healthModel;
    public Mana mana;

    public string GetID() => gameObject.name;
    public object ToData(){
        return new EnemyData{
            healthModel = healthModel,
            mana = mana,
        };
    }

    public void FromData(object data){
        if (data is EnemyData enemyData){
            healthModel = enemyData.healthModel;
            mana = enemyData.mana;
        }
    }
}