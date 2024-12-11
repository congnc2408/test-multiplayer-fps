using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Runtime.CompilerServices;

public class RoomList : MonoBehaviourPunCallbacks
{

    public static RoomList Instance;
    public GameObject roomManagerGameObject;
    public RoomManager roomManager;
    [Header("UI")]
    public Transform roomListParent;
    public GameObject roomListItemPrefab;

    private List<RoomInfo> cacheRoomList = new List<RoomInfo>();

    public void ChangeRoomToCreateName(string _roomName)
    {
        roomManager.roomNameToJoin = _roomName;

    }


    void Awake()
    {
        Instance = this;
    }

    IEnumerator Start()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }
        yield return new WaitUntil(() => !PhotonNetwork.IsConnected);

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomLst)
    {
        if (cacheRoomList.Count <= 0)
        {
            cacheRoomList = roomLst;
        }
        else
        {
            foreach (var room in roomLst)
            {
                for (int i = 0; i < cacheRoomList.Count; i++)
                {
                    if (cacheRoomList[i].Name == room.Name)
                    {
                        List<RoomInfo> newlst = cacheRoomList;
                        if (room.RemovedFromList)
                        {
                            newlst.Remove(newlst[i]);
                        }
                        else
                        {
                            newlst[i] = room;
                        }

                        cacheRoomList = newlst;
                    }
                }
            }
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        foreach (Transform RoomItem in roomListParent)
        {
            Destroy(RoomItem.gameObject);
        }

        foreach (var room in cacheRoomList)
        {
            GameObject roomItem = Instantiate(roomListItemPrefab, roomListParent);
            roomItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = room.Name;
            roomItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = room.PlayerCount + "/10";
            roomItem.GetComponent<RoomItemButton>().RoomName = room.Name;
        }
    }

    public void JoinRoomByName(string _name)
    {
        roomManager.roomNameToJoin = _name;
        roomManagerGameObject.SetActive(true);
        gameObject.SetActive(false);
    }

}
