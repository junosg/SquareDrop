using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceCreateRoomState : InterfaceBaseState
{
    public override void EnterState(InterfaceManager interfaceManager)
    {
        interfaceManager.createRoom_Canvas.gameObject.SetActive(true);
        interfaceManager.createRoom_RoomNameField.text = null;
        interfaceManager.createRoom_MaxPlayersField.text = null;
    }

    public override void UpdateState(InterfaceManager interfaceManager)
    {
        if (interfaceManager.createRoom_RoomNameField.text.Length == 0) {
            interfaceManager.createRoom_CreateRoomButton.interactable = false;
        } else {
            interfaceManager.createRoom_CreateRoomButton.interactable = true;
        }
    }

    public override void ExitState(InterfaceManager interfaceManager)
    {
        interfaceManager.createRoom_Canvas.gameObject.SetActive(false);
    }

    public override void CheckSwitchState(InterfaceManager interfaceManager)
    {
        interfaceManager.createRoom_CreateRoomButton.onClick.AddListener(() => {
            interfaceManager.SwitchState(interfaceManager.roomState);
        });

        interfaceManager.createRoom_BackButton.onClick.AddListener(() => {
            interfaceManager.SwitchState(interfaceManager.menuState);
        });
    }

    public override void OnConnectedToMaster(InterfaceManager interfaceManager)
    {
    }

    public override void OnDisconnected(InterfaceManager interfaceManager)
    {
        interfaceManager.SwitchState(interfaceManager.menuState);
    }
    public override void OnCreateRoomSuccess(InterfaceManager interfaceManager)
    {
        interfaceManager.SwitchState(interfaceManager.roomState);
    }

    public override void OnCreateRoomFailed(InterfaceManager interfaceManager)
    {
    }

    public override void OnJoinRoomSuccess(InterfaceManager interfaceManager)
    {
    }

    public override void OnJoinRoomFailed(InterfaceManager interfaceManager)
    {
    }

    public override void OnJoinRandomRoomFailed(InterfaceManager interfaceManager)
    {
    }

    public override void OnPlayerEnteredRoom(InterfaceManager interfaceManager)
    {
    }

    public override void OnPlayerLeftRoom(InterfaceManager interfaceManager)
    {
    }
}
