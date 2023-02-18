using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System.Collections.Generic;


public class NetworkManager : MonoBehaviourPunCallbacks
{
#region NetworkManagerFunctions
    public void ConnectUsingSettings()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CreateRoom(string roomName = null, byte maxPlayer = 4, int playerTtl = 5, int emptyRoomTtl = 5)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayer;
        roomOptions.PlayerTtl = playerTtl;
        roomOptions.EmptyRoomTtl = emptyRoomTtl;

        PhotonNetwork.CreateRoom(roomName, roomOptions, null);
    }

    public void JoinSpecificRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void JoinRandomRoom(string roomName)
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void SetPlayerName(string value)
    {
        PhotonNetwork.NickName = value;
    }

    public void SyncScene()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void LoadLevel(string levelName)
    {
        if (PhotonNetwork.IsMasterClient) {
            PhotonNetwork.LoadLevel("GameScene");
        }
    }

    public bool IsConnectedAndReady()
    {
        return PhotonNetwork.IsConnectedAndReady;
    }

    public bool IsConnected()
    {
        return PhotonNetwork.IsConnected;
    }

    public bool IsMasterClient()
    {
        return PhotonNetwork.IsMasterClient;
    }

    public bool IsMine()
    {
        return photonView.IsMine;
    }

    public bool InRoom()
    {
        return PhotonNetwork.InRoom;
    }

    public string RoomName()
    {
        return PhotonNetwork.CurrentRoom.Name;
    }

    public Dictionary<int, string> Players()
    {   
        Dictionary<int, string> returnValue = new Dictionary<int, string>();

        foreach (var item in PhotonNetwork.CurrentRoom.Players)
        {
            returnValue.Add(item.Key, item.Value.NickName);
        }

        return returnValue;
    }
#endregion

#region ConnectedDelegate
    public delegate void OnConnectedDelegate();
    public static OnConnectedDelegate connectedDelegate;

    public override void OnConnectedToMaster()
    {
        connectedDelegate();
    }
#endregion

#region  DisconnectedDelegate
    public delegate void OnDisconnectedDelegate();
    public static OnDisconnectedDelegate disconnectedDelegate;
    public override void OnDisconnected(DisconnectCause cause)
    {
        disconnectedDelegate();
    }
#endregion

#region CreateRoomSuccessDelegate
    public delegate void OnCreateRoomSuccessDelegate();
    public static OnCreateRoomSuccessDelegate createRoomSuccessDelegate;
    public override void OnCreatedRoom()
    {
        createRoomSuccessDelegate();
    }
#endregion

#region CreateRoomFailedDelegate
    public delegate void OnCreateRoomFailedDelegate(short returnCode, string message);
    public static OnCreateRoomFailedDelegate createRoomFailedDelegate;
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        createRoomFailedDelegate(returnCode, message);
    }
#endregion

#region JoinRoomSuccessDelegate
    public delegate void OnJoinRoomSuccessDelegate();
    public static OnJoinRoomSuccessDelegate joinRoomSuccessDelegate;
    public override void OnJoinedRoom()
    {
        joinRoomSuccessDelegate();
    }
#endregion

#region JoinRoomFailedDelegate
    public delegate void OnJoinRoomFailedDelegate(short returnCode, string message);
    public static OnJoinRoomFailedDelegate joinRoomFailedDelegate;
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        joinRoomFailedDelegate(returnCode, message);
    }
#endregion

#region JoinRandomRoomFailedDelegate
    public delegate void OnJoinRandomRoomFailedDelegate(short returnCode, string message);
    public static OnJoinRandomRoomFailedDelegate joinRandomRoomFailedDelegate;
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        joinRandomRoomFailedDelegate(returnCode, message);
    }
    #endregion

#region PlayerEnteredRoom
    public delegate void OnPlayerEnteredRoomDelegate();
    public static OnPlayerEnteredRoomDelegate playerEnteredRoomDelegate;
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        playerEnteredRoomDelegate();
    }
#endregion

#region PlayerLeftRoom
    public delegate void OnPlayerLeftRoomDelegate();
    public static OnPlayerLeftRoomDelegate playerLeftRoomDelegate;
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        playerLeftRoomDelegate();
    }
#endregion


#region LeftRoom
    public delegate void OnLeftRoomDelegate();
    public static OnLeftRoomDelegate leftRoomDelegate;
    public override void OnLeftRoom()
    {
        leftRoomDelegate();
    }
#endregion
}

