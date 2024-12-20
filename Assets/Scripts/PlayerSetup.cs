using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;


public class PlayerSetup : MonoBehaviour
{
    public Movement movement;
    public GameObject camera;
    public string nickname;
    //public TextMeshProUGUI timeRemaining;
    public Transform tpWeaponHolder;
    public TextMeshPro nicknameText;
    public Canvas playerScreen;
    public Canvas crosshairHolder;



    public void IsLocalPlayer()
    {
        tpWeaponHolder.gameObject.SetActive(false);
        movement.enabled = true;
        camera.SetActive(true);
        playerScreen.gameObject.SetActive(true);
        crosshairHolder.gameObject.SetActive(true);
        //timeRemaining.gameObject.SetActive(true);
    }

    public void Pause()
    {
        movement.enabled = false;
        camera.SetActive(false);
    }

    [PunRPC]
    public void SetTPWeapon(int _weaponIndex)
    {
        foreach (Transform _weapon in tpWeaponHolder)
        {
            _weapon.gameObject.SetActive(false);
        }
        tpWeaponHolder.GetChild(_weaponIndex).gameObject.SetActive(true);
    }

    [PunRPC]
    public void SetNickname(string _name)
    {
        nickname = _name;
        nicknameText.text = nickname;
        //Debug.Log("name: " + _name);
    }

    [PunRPC]
    public void TimeRemaining(TextMeshProUGUI timerText, float remainingTime)
    {
        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if (remainingTime <= 0)
        {
            timerText.text = "00:00";
            Application.Quit();
            Debug.Log("End");
        }
    }


}
