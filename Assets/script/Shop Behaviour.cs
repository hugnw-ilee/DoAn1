using System.Collections;
using System.Collections.Generic;
using CharactersModels;
using PlayerPresenter;
using UnityEngine;

public class ShopBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            if (shopPanel != null){
                shopPanel.SetActive(false);
            }
        }
    }

    public void Sell(GameObject ply){
        Money playerMoney = ply.GetComponent<Player>().money;
        PlayerHealth playerHealth = ply.GetComponent<PlayerHealth>();
        int curMoney = playerMoney.curMoney;
        if(curMoney - 50 >= 0){
            playerMoney.curMoney -= 50;
            playerHealth.HealthPlus(50);
            Debug.Log("health plus");
        }
    }
}
