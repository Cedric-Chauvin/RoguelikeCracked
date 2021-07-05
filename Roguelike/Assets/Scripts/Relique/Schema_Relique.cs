using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Schema_Relique : MonoBehaviour
{
    public List<Relic> List_Relics = new List<Relic>();

    public int IndexList;

    private PlayerManager P_Manager;
    private PlayerMovement P_Movement;
    public CinemachineVirtualCamera CineCamera_;
    private GameObject PlayerPref;
    public GameObject CineCamera;

    private void Start()
    {   
        PlayerPref = GameObject.Find("Player");
        CineCamera = GameObject.Find("Cinemachine_VirtualCam");
        CineCamera_ = CineCamera.GetComponent<CinemachineVirtualCamera>();
        P_Manager = PlayerPref.GetComponent<PlayerManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            ActivateRel();
        }
    }
    public void ActivateRel()
    {
        P_Manager.PlayerSize = P_Manager.PlayerSize * (1 + List_Relics[IndexList].ModifSize);
        P_Manager.PlayerSpeed = P_Manager.PlayerSpeed * (1 + List_Relics[IndexList].ModifSpeed);
        PlayerPref.transform.localScale = new Vector3(PlayerPref.transform.localScale.x* P_Manager.PlayerSize, PlayerPref.transform.localScale.y*P_Manager.PlayerSize, 1);
        P_Manager.CameraFOV = P_Manager.CameraFOV * (1 + List_Relics[IndexList].ModifFOV);
        CineCamera_.m_Lens.OrthographicSize = P_Manager.CameraFOV;
        P_Manager.BulletFireRate = P_Manager.BulletFireRate * ( 1 + List_Relics[IndexList].ModifBulletFireRate);
        P_Manager.BulletDamage = P_Manager.BulletDamage * (1 + List_Relics[IndexList].ModifBulletDamage);
        P_Manager.BulletSpeed = P_Manager.BulletSpeed * (1 + List_Relics[IndexList].ModifBulletSpeed);
        P_Manager.BulletDuration = P_Manager.BulletDuration * (1 + List_Relics[IndexList].ModifBulletDuration);
        P_Manager.DetectionRange = P_Manager.DetectionRange * (1 + List_Relics[IndexList].ModifDetectionRange);
        Destroy(this.gameObject);
    }
}
