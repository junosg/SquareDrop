using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject PlayerObject;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(PlayerObject.name, Vector3.zero + new Vector3(Random.Range(0, 2), 0, Random.Range(0, 2)) , Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
