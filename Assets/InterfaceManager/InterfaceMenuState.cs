using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceMenuState : InterfaceBaseState
{
    private bool connectedToMaster = false;
    public override void EnterState(InterfaceManager interfaceManager)
    {
        interfaceManager.menu_Canvas.gameObject.SetActive(true);
        interfaceManager.menu_Canvas.worldCamera = Camera.main;

        interfaceManager.menu_CreateRoomButton.interactable = false;
        interfaceManager.menu_JoinRoomButton.interactable = false;

        if (interfaceManager.networkManager.IsConnected() == false) {
            interfaceManager.networkManager.ConnectUsingSettings();
        } else {
            interfaceManager.menu_CreateRoomButton.interactable = true;
            interfaceManager.menu_JoinRoomButton.interactable = true;
        }

        if (interfaceManager.networkManager.InRoom())
            interfaceManager.networkManager.LeaveRoom();
    }

    public override void UpdateState(InterfaceManager interfaceManager)
    {
        if (connectedToMaster) {
            interfaceManager.networkManager.SetPlayerName(interfaceManager.menu_PlayerNameField.text);

            if (interfaceManager.menu_PlayerNameField.text.Length == 0) {
                interfaceManager.menu_CreateRoomButton.interactable = false;
                interfaceManager.menu_JoinRoomButton.interactable = false;
                interfaceManager.createRoom_CreateRoomButton.interactable = false;
            } else {
                interfaceManager.menu_CreateRoomButton.interactable = true;
                interfaceManager.menu_JoinRoomButton.interactable = true;
                interfaceManager.createRoom_CreateRoomButton.interactable = true;
            }
        }
    }

    public override void ExitState(InterfaceManager interfaceManager)
    {
        interfaceManager.menu_Canvas.gameObject.SetActive(false);
    }

    public override void CheckSwitchState(InterfaceManager interfaceManager)
    {
        interfaceManager.menu_CreateRoomButton.onClick.AddListener(() => {
            interfaceManager.SwitchState(interfaceManager.createRoomState);
        });

        interfaceManager.menu_JoinRoomButton.onClick.AddListener(() => {
            interfaceManager.SwitchState(interfaceManager.joinRoomState);
        });

        interfaceManager.menu_ExitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }

    public override void OnConnectedToMaster(InterfaceManager interfaceManager)
    {
        connectedToMaster = true;
    }

    public override void OnDisconnected(InterfaceManager interfaceManager)
    {
        connectedToMaster = false;
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
