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

    public string roomNameJoin = "test";
    [HideInInspector]
    public int kills = 0;
    [HideInInspector]
    public int deaths = 0;

    public void ChangeNickName(string _name)
    {
        nickname = _name;
        // Debug.Log("nickname: " + nickname);
    }

    public void JoinRoomButtonPress()
    {
        Debug.Log("Connecting...");
        PhotonNetwork.ConnectUsingSettings();
        nameUI.SetActive(false);
        connectingUI.SetActive(true);
    }

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to server");

        PhotonNetwork.JoinLobby();
    }


    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        PhotonNetwork.JoinOrCreateRoom("test", null, null);
        Debug.Log("we're connected and in the lobby");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        roomCam.SetActive(false);
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {

        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];

        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, quaternion.identity);
        _player.GetComponent<PlayerSetup>().IsLocalPlayer();
        _player.GetComponent<Health>().isLocalPlayer = true;
        _player.GetComponent<PhotonView>().RPC("SetNickname", RpcTarget.All, nickname);
        PhotonNetwork.LocalPlayer.NickName = nickname;
    }

    public void SetHashes()
    {
        try
        {
            Hashtable hashs = PhotonNetwork.LocalPlayer.CustomProperties;
            hashs["kill"] = kills;
            hashs["death"] = deaths;
            PhotonNetwork.LocalPlayer.SetCustomProperties(hashs);
        }
        catch (System.Exception)
        {

            throw;
        }
    }
}
