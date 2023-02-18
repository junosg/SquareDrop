using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InterfaceBaseState
{
    public abstract void EnterState(InterfaceManager interfaceManager);
    public abstract void UpdateState(InterfaceManager interfaceManager);
    public abstract void ExitState(InterfaceManager interfaceManager);
    public abstract void CheckSwitchState(InterfaceManager interfaceManager);

    public abstract void OnConnectedToMaster(InterfaceManager interfaceManager);
    public abstract void OnDisconnected(InterfaceManager interfaceManager);
    public abstract void OnCreateRoomSuccess(InterfaceManager interfaceManager);
    public abstract void OnCreateRoomFailed(InterfaceManager interfaceManager);
    public abstract void OnJoinRoomSuccess(InterfaceManager interfaceManager);
    public abstract void OnJoinRoomFailed(InterfaceManager interfaceManager);
    public abstract void OnJoinRandomRoomFailed(InterfaceManager interfaceManager);
    public abstract void OnPlayerEnteredRoom(InterfaceManager interfaceManager);
    public abstract void OnPlayerLeftRoom(InterfaceManager interfaceManager);

}
