using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface;
using System;

namespace Interface
{
    public interface IStat
    {
        public int RestoreMax();
        public int Minus(int value);
        public int Plus(int value);
    }

    public interface IScore
    {
        public int ScoreUp(int value);
        public int GetScore();
    }

    public interface IMoney
    {
        public int MoneyPlus(int value);
        public int Purchase(int value);
    }

}

namespace CharactersModels
{
    [Serializable]
    public class Location{
        public float[] position = new float[3];
        public void LocationSet(float x, float y, float z){
            position[0] = x;
            position[1] = y;
            position[2] = z; 
        }

        public void GetLocation(Transform tf){
            Vector3 loadPos = new (position[0],position[1],position[2]);
            tf.position = loadPos;
        }       
    }

    [Serializable]
    public class HealthModel 
    {    
        public int maxHealth;
        public int minHealth = 0;
        public int curHealth;

        public int RestoreMax() => curHealth = maxHealth;
        
        public int Minus(int value) => curHealth = Mathf.Clamp(curHealth - value, minHealth, maxHealth);

        public int Plus(int value) => curHealth = Mathf.Clamp(curHealth + value, minHealth, maxHealth);

    }

    [Serializable]
    public class Mana: IStat{
        public int maxMana;
        public int minMana = 0;
        public int curMana;
        public bool invincible;

        public int RestoreMax() => curMana = maxMana;
        public int Minus(int value) => curMana = Mathf.Clamp(curMana - value, minMana, maxMana);
        public int Plus(int value) => curMana = Mathf.Clamp(curMana + value, minMana, maxMana);
    }

    [Serializable]
    public class Score : IScore{
        public int curScore = 0;

        // Cần chỉnh lại levelScore
        public int levelScore = 0;

        public int ScoreUp(int value) => levelScore += value;

        public int GetScore() => curScore += levelScore;
    }

    [Serializable]
    public class Money: IMoney{

        public int curMoney = 0;
        public int MoneyPlus(int value) => curMoney += value;

        public int Purchase(int value){
            return curMoney;
        }
    }

    [Serializable]
    public class Item{

    }
}

