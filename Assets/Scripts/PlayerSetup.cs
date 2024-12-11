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


    public Transform TPweapponHolder;

    public TextMeshPro nicknameText;

    public void IsLocalPlayer()
    {
        TPweapponHolder.gameObject.SetActive(false);
        movement.enabled = true;
        camera.SetActive(true);
    }

    [PunRPC]
    public void SetTPWeapon(int _weaponIndex)
    {
        foreach (Transform _weapon in TPweapponHolder)
        {
            _weapon.gameObject.SetActive(false);
        }
        TPweapponHolder.GetChild(_weaponIndex).gameObject.SetActive(true);
    }

    [PunRPC]
    public void SetNickname(string _name)
    {
        nickname = _name;
        nicknameText.text = nickname;
        Debug.Log("name: " + _name);
    }


}
