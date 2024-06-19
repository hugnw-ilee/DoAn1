using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonClickHandler : MonoBehaviour
{
    public MenuController menuController;
    private Button btn;
    private TMP_Text tmp_Text;

    private void Start() {
        btn = GetComponent<Button>();
        tmp_Text = GetComponentInChildren<TMP_Text>();
        btn.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        menuController.NewGameYes();
        string uiName = tmp_Text.text;
        CoroutineHandler.Instance.StartCoroutine(WaitAndLoadData(uiName));
    }

    private IEnumerator WaitAndLoadData(string name)
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().isLoaded);
        SaveSystem.Instance.LoadData(name);
    }
}
