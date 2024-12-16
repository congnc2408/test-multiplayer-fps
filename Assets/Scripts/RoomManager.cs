using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.Mathematics;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance { get; private set; }

    public GameObject player;
    [Space]
    public Transform[] spawnPoints;
    [Space]
    public GameObject roomCam;
    private string nickname = "unnamed";
    [Space]
    public GameObject nameUI;
    public GameObject connectingUI;

    [HideInInspector]
    public int kills = 0;
    [HideInInspector]
    public int deaths = 0;

    public string roomNameToJoin = "test";

    void Awake()
    {
        Instance = this;
    }


    public void ChangeNickName(string _name)
    {
        nickname = _name;
        // Debug.Log("nickname: " + nickname);
    }

    public void JoinRoomButtonPress()
    {
        Debug.Log("Connecting...");
        PhotonNetwork.JoinOrCreateRoom(roomNameToJoin, null, null);
        nameUI.SetActive(false);
        connectingUI.SetActive(true);
    }


    // public override void OnConnectedToMaster()
    // {
    //     base.OnConnectedToMaster();
    //     Debug.Log("Connected to server");

    //     PhotonNetwork.JoinLobby();
    // }


    // public override void OnJoinedLobby()
    // {
    //     base.OnJoinedLobby();
    //     Debug.Log("we're in the lobby");
    //     PhotonNetwork.JoinOrCreateRoom(roomNameToJoin, null, null);
    // }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("We're connected and in a room");
        roomCam.SetActive(false);
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {

        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];

        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, quaternion.identity);
        _player.GetComponent<PlayerSetup>().IsLocalPlayer();
        _player.GetComponent<Health>().isLocalPlayer = true;
        //_player.GetComponent<PlayerSetup>().isTPPlayer = true;
        _player.GetComponent<PhotonView>().RPC("SetNickname", RpcTarget.All, nickname);
        PhotonNetwork.LocalPlayer.NickName = nickname;
    }

    public void SetHashes()
    {
        try
        {
            Hashtable hashs = PhotonNetwork.LocalPlayer.CustomProperties;
            hashs["kills"] = kills;
            hashs["deaths"] = deaths;
            PhotonNetwork.LocalPlayer.SetCustomProperties(hashs);
        }
        catch (System.Exception)
        {

            throw;
        }
    }
}
