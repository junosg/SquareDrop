using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Realtime;

public class PlayerManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject playerPrefab;
    public float movespeed = 4f;

    [HideInInspector]
    public Rigidbody rigidBody;
    [HideInInspector]
    public CapsuleCollider capsuleCollider;
    [HideInInspector]
    public GameObject cameraFollowTarget;

    public PlayerBaseState currentState;
    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerMoveState moveState = new PlayerMoveState();

    private InputManager _inputManager;
    [HideInInspector]
    public InputAction move;
    [HideInInspector]
    public PhotonView photonView;

    void OnEnable()
    {
        move = _inputManager.Player.Move;
        move.Enable();
    }

    void OnDisable()
    {
        move.Disable();
    }

    void Awake()
    {
        _inputManager = new InputManager();
    }
    void Start()
    {
        photonView = GetComponent<PhotonView>();

        if (!photonView.IsMine) return;

        rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;

        capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
        cameraFollowTarget = GameObject.Find("CameraFollowTarget");

        currentState = idleState;
        currentState.EnterState(this);
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine) return;

        currentState.FixedUpdateState(this);
    }
    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;

        currentState.CheckSwitchState(this);
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState state)
    {
        if (!photonView.IsMine) return;

        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    public void IgnoreOwnedProjectiles()
    {
    }
}
