using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public bool isLocalPlayer;
    public RectTransform healthBar;
    private float originalHealthBarSize;
    [Header("UI")]
    public TextMeshProUGUI healthText;
    public Image healColor;

    void Start()
    {
        originalHealthBarSize = healthBar.sizeDelta.x;
        healColor.color = Color.red;
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
        // int presentDeath = RoomManager.Instance.deaths;
        // Debug.Log("presentDeath: " + presentDeath);
        if (health <= 0)
        {
            if (isLocalPlayer)
            {
                RoomManager.Instance.SpawnPlayer();
                RoomManager.Instance.deaths++;
                RoomManager.Instance.SetHashes();
                // int plusDeath = RoomManager.Instance.deaths++;
                // Debug.Log("plusDeath: " + plusDeath);
                // if (presentDeath < plusDeath)
                // {
                //     //TimeRemaining.timeRemaining.remainingTime -= 20;
                //     float time = TimeRemaining.timeRemaining.remainingTime - 20;
                //     TimeRemaining.timeRemaining.setRemainingTime(time);
                //     Debug.Log("time down: " + time);
                //}
            }
            Destroy(gameObject);
        }
    }


}
