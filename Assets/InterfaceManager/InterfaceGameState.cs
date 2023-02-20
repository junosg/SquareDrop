using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceGameState : InterfaceBaseState
{
    public override void EnterState(InterfaceManager interfaceManager)
    {
        interfaceManager.networkManager.LoadLevel("SnowballDrop");
    }

    public override void UpdateState(InterfaceManager interfaceManager)
    {
    }

    public override void ExitState(InterfaceManager interfaceManager)
    {

    }

    public override void CheckSwitchState(InterfaceManager interfaceManager)
    {
    }

    public override void OnConnectedToMaster(InterfaceManager interfaceManager)
    {
    }

    public override void OnDisconnected(InterfaceManager interfaceManager)
    {
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
