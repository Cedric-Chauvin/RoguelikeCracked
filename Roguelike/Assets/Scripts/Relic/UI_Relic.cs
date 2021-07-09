using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Relic : MonoBehaviour
{
    public TextMeshProUGUI[] RelicTitle = new TextMeshProUGUI[3];
    public TextMeshProUGUI[] RelicDescription = new TextMeshProUGUI[3];
    public Image[] RelicSprite = new Image[3];

    public Button RerollButton;
    public GameObject Relic_Menu;

    private GameObject PlayerPref;
    private PlayerManager P_Manager;

    public List<Relic> List_Relics = new List<Relic>();
    public List<Relic> List3Relics = new List<Relic>();

    public int RerollRemaining;

    private void Start()
    {
        PlayerPref = GameObject.Find("Player");
        P_Manager = PlayerPref.GetComponent<PlayerManager>();
        List3Relics = Reroll();
    }

    public void ActivateRel(int IndexList)
    {
        P_Manager.PlayerSize = P_Manager.PlayerSize * (1 + List3Relics[IndexList].ModifSize);
        P_Manager.PlayerSpeed = P_Manager.PlayerSpeed * (1 + List3Relics[IndexList].ModifSpeed);
        PlayerPref.transform.localScale = new Vector3(PlayerPref.transform.localScale.x * P_Manager.PlayerSize, PlayerPref.transform.localScale.y * P_Manager.PlayerSize, 1);
        P_Manager.CameraFOV = P_Manager.CameraFOV * (1 + List3Relics[IndexList].ModifFOV);
        P_Manager.BulletFireRate = P_Manager.BulletFireRate * (1 + List3Relics[IndexList].ModifBulletFireRate);
        P_Manager.BulletDamage = P_Manager.BulletDamage * (1 + List3Relics[IndexList].ModifBulletDamage);
        P_Manager.BulletSpeed = P_Manager.BulletSpeed * (1 + List3Relics[IndexList].ModifBulletSpeed);
        P_Manager.BulletDuration = P_Manager.BulletDuration * (1 + List3Relics[IndexList].ModifBulletDuration);
        P_Manager.DetectionRange = P_Manager.DetectionRange * (1 + List3Relics[IndexList].ModifDetectionRange);
        RerollRemaining = P_Manager.NbReroll;
        Time.timeScale = 1;
        Relic_Menu.SetActive(false);
    }

    private void OnEnable()
    {
        List3Relics = Reroll();
    }

    public void RerollAction()
    {
        if (RerollRemaining > 1)
        {
            RerollRemaining--;
            List3Relics = Reroll();
        }
        else if(RerollRemaining == 1)
        {
            RerollRemaining--;
            List3Relics = Reroll();
            RerollButton.gameObject.SetActive(false);
        }
    }

    public List<Relic> Reroll()
    {
        List<Relic> List3Relics = new List<Relic>();
        int Relic0 = Random.Range(0, List_Relics.Count);
        List3Relics.Add(List_Relics[Relic0]);
        int Relic1 = Random.Range(0, List_Relics.Count);
        if (Relic1 == Relic0)
        {
            while (Relic1 == Relic0)
            {
                Relic1 = Random.Range(0, List_Relics.Count);
            }
        }
        List3Relics.Add(List_Relics[Relic1]);
        int Relic2 = Random.Range(0, List_Relics.Count);
        if (Relic2 == Relic0 || Relic2 == Relic1)
        {
            while (Relic2 == Relic0 || Relic2 == Relic1)
            {
                Relic2 = Random.Range(0, List_Relics.Count);
            }
        }
        List3Relics.Add(List_Relics[Relic2]);

        for (int i = 0; i < List3Relics.Count; i++)
        {
            RelicTitle[i].text = List3Relics[i].RelicName;
            RelicDescription[i].text = List3Relics[i].RelicDescription;
            RelicSprite[i].sprite = List3Relics[i].RelicSprite;
        }
        return List3Relics;
    }
}
