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


    public Transform tpWeaponHolder;
    //public Transform localWeaponHolder;

    public TextMeshPro nicknameText;

    bool isTPPlayer;

    public void IsLocalPlayer()
    {
        tpWeaponHolder.gameObject.SetActive(false);
        movement.enabled = true;
        camera.SetActive(true);
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
        Debug.Log("name: " + _name);
    }


}
