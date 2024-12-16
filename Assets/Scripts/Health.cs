using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public bool isLocalPlayer;
    public RectTransform healthBar;
    private float originalHealthBarSize;
    [Header("UI")]
    public TextMeshProUGUI healthText;


    void Start()
    {
        originalHealthBarSize = healthBar.sizeDelta.x;
    }

    void Update()
    {
        healthBar.sizeDelta = new Vector2(originalHealthBarSize * health / 100f, healthBar.sizeDelta.y);
    }

    [PunRPC]
    public void TakeDamage(int _damage)
    {
        health -= _damage;
        healthBar.sizeDelta = new Vector2(originalHealthBarSize * health / 100f, healthBar.sizeDelta.y);
        healthText.text = health.ToString();
        if (health <= 0)
        {
            if (isLocalPlayer)
            {
                RoomManager.Instance.SpawnPlayer();
                RoomManager.Instance.deaths++;
                RoomManager.Instance.SetHashes();

            }


            Destroy(gameObject);
        }
    }


}
