using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schema_Relique : MonoBehaviour
{
    private GameObject Canvas;
    private GameObject UI_Relic;

    private void Start()
    {
        Canvas = GameObject.Find("Canvas");
        UI_Relic = Canvas.GetComponent<InactiveUIGameObject>().InactiveUI[0];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            UI_Initializing();
        }
    }

    public void UI_Initializing()
    {
        Time.timeScale = 0;
        UI_Relic.SetActive(true);
        Destroy(gameObject);
    }
}
