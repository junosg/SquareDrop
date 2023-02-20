using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;
using Photon.Realtime;

public class CameraManager : MonoBehaviour
{
    private GameObject _followTarget;
    private GameObject _lookTarget;
    private CinemachineVirtualCamera _virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        _virtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        _followTarget = GameObject.Find("CameraFollowTarget");
        _lookTarget = GameObject.FindGameObjectWithTag("Player");

        if (PhotonNetwork.IsConnected && !_lookTarget.GetPhotonView().IsMine) return;

        _virtualCamera.LookAt = _lookTarget.transform;

        _virtualCamera.m_Lens.FieldOfView = 35f;

        _virtualCamera.Follow = _followTarget.transform;
    }
}
