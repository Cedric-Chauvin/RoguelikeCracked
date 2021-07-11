using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Relic : MonoBehaviour
{
    [Header("UI Object")]
    public TextMeshProUGUI[] RelicTitle = new TextMeshProUGUI[3];
    public TextMeshProUGUI[] RelicDescription = new TextMeshProUGUI[3];
    public TextMeshProUGUI[] RelicLVL = new TextMeshProUGUI[3];
    public Image[] RelicSprite = new Image[3];
    public Button RerollButton;
    public GameObject Relic_Menu;

    private PlayerManager P_Manager;

    [Header("List Relics")]
    private List<Relic> List_Relics_Pool = new List<Relic>();
    private List<Relic> List3Relics = new List<Relic>();
    public All_RelicLVL List_AllRelicLVL;

    public int RerollRemaining;

    private void Start()
    {
        for (int i = 0; i < List_AllRelicLVL.List_AllRelicsLVL[0].List_RelicsLVL.Count; i++)
        {
            List_Relics_Pool.Add(List_AllRelicLVL.List_AllRelicsLVL[0].List_RelicsLVL[i]);
        }
        P_Manager = GameObject.Find("Player").GetComponent<PlayerManager>();
        List3Relics = Reroll();
    }

    public void ActivateRel(int IndexList)
    {
        Relic SelectedRelic = List3Relics[IndexList];
        int RelicLVL = SelectedRelic.RelicLVL;
        int IndexSelected = List_Relics_Pool.IndexOf(SelectedRelic);

        if (RelicLVL < List_AllRelicLVL.List_AllRelicsLVL.Count)
        {
            List_Relics_Pool[IndexSelected] = List_AllRelicLVL.List_AllRelicsLVL[RelicLVL].List_RelicsLVL[IndexSelected];
        }
        P_Manager.ApplyRelic(SelectedRelic);
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
        int Relic0 = Random.Range(0, List_Relics_Pool.Count);
        List3Relics.Add(List_Relics_Pool[Relic0]);
        int Relic1 = Random.Range(0, List_Relics_Pool.Count);
        if (Relic1 == Relic0)
        {
            while (Relic1 == Relic0)
            {
                Relic1 = Random.Range(0, List_Relics_Pool.Count);
            }
        }
        List3Relics.Add(List_Relics_Pool[Relic1]);
        int Relic2 = Random.Range(0, List_Relics_Pool.Count);
        if (Relic2 == Relic0 || Relic2 == Relic1)
        {
            while (Relic2 == Relic0 || Relic2 == Relic1)
            {
                Relic2 = Random.Range(0, List_Relics_Pool.Count);
            }
        }
        List3Relics.Add(List_Relics_Pool[Relic2]);

        for (int i = 0; i < List3Relics.Count; i++)
        {
            RelicTitle[i].text = List3Relics[i].RelicName;
            RelicDescription[i].text = List3Relics[i].RelicDescription;
            RelicLVL[i].text = List3Relics[i].RelicLVLText;
            RelicSprite[i].sprite = List3Relics[i].RelicSprite;
        }
        return List3Relics;
    }
}