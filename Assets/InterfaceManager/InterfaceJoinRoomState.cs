using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceJoinRoomState : InterfaceBaseState
{
  public override void EnterState(InterfaceManager interfaceManager)
    {
        interfaceManager.joinRoom_Canvas.gameObject.SetActive(true);
        interfaceManager.joinRoom_RoomNameField.text = null;

        interfaceManager.joinRoom_Canvas.worldCamera = Camera.main;
    }

    public override void UpdateState(InterfaceManager interfaceManager)
    {
        if (interfaceManager.joinRoom_RoomNameField.text.Length == 0) {
            interfaceManager.joinRoom_JoinRoomButton.interactable = false;
        } else {
            interfaceManager.joinRoom_JoinRoomButton.interactable = true;
        }
    }

    public override void ExitState(InterfaceManager interfaceManager)
    {
        interfaceManager.joinRoom_Canvas.gameObject.SetActive(false);
    }

    public override void CheckSwitchState(InterfaceManager interfaceManager)
    {
        interfaceManager.joinRoom_JoinRoomButton.onClick.AddListener(() => {
            interfaceManager.SwitchState(interfaceManager.roomState);
        });

        interfaceManager.joinRoom_BackButton.onClick.AddListener(() => {
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
    }

    public override void OnCreateRoomFailed(InterfaceManager interfaceManager)
    {
    }

    public override void OnJoinRoomSuccess(InterfaceManager interfaceManager)
    {
    }

    public override void OnJoinRoomFailed(InterfaceManager interfaceManager)
    {
        interfaceManager.SwitchState(interfaceManager.menuState);
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

    public override void OnLeftRoom(InterfaceManager interfaceManager)
    {
    }
}
