using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public static InGameManager Instance { get; private set; }

    [SerializeField] private PlayerSetup player;
    [SerializeField] private GameObject ingameMenu;

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        //player.Pause();
    }

    public void Continue()
    {
        Time.timeScale = 1.0f;
        player.enabled = true;
        player.IsLocalPlayer();
    }


    void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ingameMenu.SetActive(true);
            Pause();
            Debug.Log("abc");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        // else
        // {
        //     ingameMenu.SetActive(false);
        //     Continue();
        // }
    }

    public void DisableMenu()
    {
        ingameMenu.SetActive(false);
        Continue();
        Debug.Log("click");
    }

    public void quitGame()
    {
        Application.Quit();
    }

}
