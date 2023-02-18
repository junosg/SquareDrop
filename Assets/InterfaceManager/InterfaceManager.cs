using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InterfaceManager : MonoBehaviour
{
    [Header("Menu")]
    public TMP_InputField menu_PlayerNameField;
    public Canvas menu_Canvas;
    public Button menu_CreateRoomButton;
    public Button menu_JoinRoomButton;
    public Button menu_ExitButton;

    [Header("Create Room")]
    public Canvas createRoom_Canvas;
    public TMP_InputField createRoom_RoomNameField;
    public TMP_InputField createRoom_MaxPlayersField;
    public Button createRoom_CreateRoomButton;
    public Button createRoom_BackButton;
    
    [Header("Join Room")]
    public Canvas joinRoom_Canvas;
    public TMP_InputField joinRoom_RoomNameField;
    public Button joinRoom_JoinRoomButton;
    public Button joinRoom_BackButton;

    [Header("Room")]
    public Canvas room_Canvas;
    public TMP_InputField room_RoomCodeField;
    public Button room_CopyCodeButton;
    public Button room_BackButton;
    public TMP_Text room_PlayerList;

    public InterfaceBaseState currentState;
    public InterfaceBaseState previousState;
    public InterfaceMenuState menuState = new InterfaceMenuState();
    public InterfaceCreateRoomState createRoomState = new InterfaceCreateRoomState();
    public InterfaceJoinRoomState joinRoomState = new InterfaceJoinRoomState();
    public InterfaceRoomState roomState = new InterfaceRoomState();

    [HideInInspector]
    public NetworkManager networkManager;

    // Start is called before the first frame update
    void Start()
    {
        networkManager = gameObject.AddComponent<NetworkManager>();

        currentState = menuState;
        currentState.EnterState(this);

        NetworkManager.connectedDelegate += OnConnectedToMaster;
        NetworkManager.disconnectedDelegate += OnDisconnected;
        NetworkManager.createRoomSuccessDelegate += OnCreateRoomSuccess;
        NetworkManager.createRoomFailedDelegate += OnCreateRoomFailed;
        NetworkManager.joinRoomSuccessDelegate += OnJoinRoomSuccess;
        NetworkManager.joinRoomFailedDelegate += OnJoinRoomFailed;
        NetworkManager.joinRandomRoomFailedDelegate += OnJoinRandomRoomFailed;
        NetworkManager.playerEnteredRoomDelegate += OnPlayerEnteredRoom;
        NetworkManager.playerLeftRoomDelegate += OnPlayerLeftRoom;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentState.CheckSwitchState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(InterfaceBaseState state)
    {
        previousState = currentState;
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    void OnConnectedToMaster()
    {
        currentState.OnConnectedToMaster(this);
    }

    void OnDisconnected()
    {
        currentState.OnDisconnected(this);
    }

    void OnCreateRoomSuccess()
    {
        currentState.OnCreateRoomSuccess(this);
    }

    void OnCreateRoomFailed(short returnCode, string message)
    {
        currentState.OnCreateRoomFailed(this);
    }

    void OnJoinRoomSuccess()
    {
        currentState.OnJoinRoomSuccess(this);
    }

    void OnJoinRoomFailed(short returnCode, string message)
    {
        currentState.OnJoinRoomFailed(this);
    }

    void OnJoinRandomRoomFailed(short returnCode, string message)
    {
        currentState.OnJoinRandomRoomFailed(this);
    }

    void OnPlayerEnteredRoom()
    {
        currentState.OnPlayerEnteredRoom(this);
    }

    void OnPlayerLeftRoom()
    {
        currentState.OnPlayerLeftRoom(this);
    }
}
