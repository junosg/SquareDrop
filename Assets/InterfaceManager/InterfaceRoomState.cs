using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceRoomState : InterfaceBaseState
{
    public override void EnterState(InterfaceManager interfaceManager)
    {
        interfaceManager.room_Canvas.gameObject.SetActive(true);
        interfaceManager.room_RoomCodeField.text = null;
        interfaceManager.room_CopyCodeButton.interactable = false;
        interfaceManager.room_BackButton.interactable = false;

        if (interfaceManager.networkManager.IsConnectedAndReady()) {
            if (interfaceManager.previousState == interfaceManager.createRoomState)
                interfaceManager.networkManager.CreateRoom(interfaceManager.createRoom_RoomNameField.text);

            if (interfaceManager.previousState == interfaceManager.joinRoomState)
                interfaceManager.networkManager.JoinSpecificRoom(interfaceManager.joinRoom_RoomNameField.text);
        }
    }

    public override void UpdateState(InterfaceManager interfaceManager)
    {
    }

    public override void ExitState(InterfaceManager interfaceManager)
    {
        interfaceManager.room_Canvas.gameObject.SetActive(false);

        if (interfaceManager.networkManager.InRoom())
            interfaceManager.networkManager.LeaveRoom();
    }

    public override void CheckSwitchState(InterfaceManager interfaceManager)
    {
        interfaceManager.room_BackButton.onClick.AddListener(() => {
            interfaceManager.SwitchState(interfaceManager.menuState);
        });
    }

    public override void OnConnectedToMaster(InterfaceManager interfaceManager)
    {
    }

    public override void OnDisconnected(InterfaceManager interfaceManager)
    {
    }

    public override void OnCreateRoomSuccess(InterfaceManager interfaceManager)
    {
        interfaceManager.room_RoomCodeField.text = interfaceManager.networkManager.RoomName();
        interfaceManager.room_CopyCodeButton.interactable = true;
        interfaceManager.room_BackButton.interactable = true;

        interfaceManager.room_PlayerList.text = DisplayPlayerList(interfaceManager.networkManager.Players());
    }

    public override void OnCreateRoomFailed(InterfaceManager interfaceManager)
    {
        interfaceManager.SwitchState(interfaceManager.menuState);
    }

    public override void OnJoinRoomSuccess(InterfaceManager interfaceManager)
    {
        interfaceManager.room_RoomCodeField.text = interfaceManager.networkManager.RoomName();
        interfaceManager.room_CopyCodeButton.interactable = true;
        interfaceManager.room_BackButton.interactable = true;

        interfaceManager.room_PlayerList.text = DisplayPlayerList(interfaceManager.networkManager.Players());
    }

    public override void OnJoinRoomFailed(InterfaceManager interfaceManager)
    {
        interfaceManager.SwitchState(interfaceManager.menuState);
    }

    public override void OnJoinRandomRoomFailed(InterfaceManager interfaceManager)
    {
        interfaceManager.SwitchState(interfaceManager.menuState);
    }

    public override void OnPlayerEnteredRoom(InterfaceManager interfaceManager)
    {
        interfaceManager.room_PlayerList.text = DisplayPlayerList(interfaceManager.networkManager.Players());
    }

    public override void OnPlayerLeftRoom(InterfaceManager interfaceManager)
    {
        interfaceManager.room_PlayerList.text = DisplayPlayerList(interfaceManager.networkManager.Players());
    }
    
    private string DisplayPlayerList(Dictionary<int, string> players)
    {
        string returnValue = "";

        foreach (var item in players)
        {
            returnValue += item.Value + "<br>";
        }

        return returnValue;
    }
    
}
