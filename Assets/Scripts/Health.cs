using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public bool isLocalPlayer;
    [Header("UI")]
    public TextMeshProUGUI healthText;




    public void TakeDamage(int _damage)
    {
        health -= _damage;
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
