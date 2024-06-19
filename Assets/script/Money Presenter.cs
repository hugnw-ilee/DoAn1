using System.Collections;
using System.Collections.Generic;
using CharactersModels;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MoneyPresenter : MonoBehaviour
{
    private Money money;
    [SerializeField] private Object moneyView;
    private string moneyDisplay = "Your Money: ";

    private void Awake() {
        money = GetComponent<Player>().money;
    }

    
    void Start()
    {
        StartCoroutine(UpdateTime());
    }

    void Update(){
        UpdateView();
    }

    private IEnumerator UpdateTime(){
        while (true){
            yield return new WaitForSeconds(60);
            money.MoneyPlus(100);
        }
    }

    private void UpdateView(){
        moneyView.GetComponent<TMP_Text>().text = moneyDisplay + money.curMoney;
    }
}
