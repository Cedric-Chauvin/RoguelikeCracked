using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schema_Relique : MonoBehaviour
{
    public GameObject UI_Relic;

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
