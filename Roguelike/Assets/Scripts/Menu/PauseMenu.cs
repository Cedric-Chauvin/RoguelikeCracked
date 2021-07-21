using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public TextMeshProUGUI[] Value = new TextMeshProUGUI[5];
    public TextMeshProUGUI WorldName;

    [Header("UI GameObject")]
    public GameObject BlackScreen;
    public GameObject PauseMenuUI;
    public GameObject PauseButton;

    private PlayerManager P_Manager;


    private void Start()
    {
        PauseMenuUI = this.gameObject;
        WorldName.text = SceneManager.GetActiveScene().name;
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
        int Minutes = (int)Time.timeSinceLevelLoad/60;
        if(Minutes != 0)
        {
            Value[0].text = "Time : " + Minutes +" min " + (int)Time.timeSinceLevelLoad % 60 + " s";
        }
        else
        {
            Value[0].text = "Time : " + (int)Time.timeSinceLevelLoad + " s";
        }

        Value[2].text = "Kill Count : " + P_Manager.KillCount;
    }

    public void OpenPauseMenu()
    {
        if (P_Manager == null)
        {
            P_Manager = GameObject.Find("Player").GetComponent<PlayerManager>();
        }
        PauseButton.SetActive(false);
        PauseMenuUI.SetActive(true);
        BlackScreen.SetActive(true);
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        BlackScreen.SetActive(false);
        PauseButton.SetActive(true);
        Time.timeScale = 1;
    }

    public void GiveUp()
    {
        
    }

    public void ShowRelics()
    {

    }

    public void ShowBonuses()
    {

    }
}
