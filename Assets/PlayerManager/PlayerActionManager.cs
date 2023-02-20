using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Realtime;

public class PlayerActionManager : MonoBehaviour
{
    [System.Serializable]
    public class Projectile {
        public string projectileName = "ProjectileName";
        public GameObject projectilePrefab;
        public float projectileEmitDelay = 0f;  
        public float projectileEmitForce = 4f;
        public float projectileLifetime = 2f;
    }

    [Header("Projectiles")]
    [SerializeField] private Projectile[] projectiles;

    private InputManager _inputManager;
    private InputAction look;
    private InputAction primaryFire;
    private InputAction secondaryFire;

    private Ray _aimDirectionRay;
    private Vector3 _aimDirection;
    private RaycastHit _rayHit;

    private PhotonView _photonView;

    // Start is called before the first frame update
    void Awake()
    {
        _inputManager =  new InputManager();
        _photonView = gameObject.GetPhotonView();
    }

    void OnEnable()
    {
        look = _inputManager.Player.Look;
        look.Enable();

        primaryFire = _inputManager.Player.PrimaryFire;
        primaryFire.Enable();

        secondaryFire = _inputManager.Player.SecondaryFire;
        secondaryFire.Enable();
    }

    void OnDisable()
    {
        look.Disable();
        primaryFire.Disable();
        secondaryFire.Disable();       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Aim();
        ImpulsedInstantiate();
    }


    void Aim()
    {
        _aimDirectionRay = Camera.main.ScreenPointToRay(look.ReadValue<Vector2>());

        if (Physics.Raycast(_aimDirectionRay, out _rayHit, 100, ~LayerMask.NameToLayer("Ground"))) {
            Debug.DrawLine(transform.position, _rayHit.point, Color.red);

            if (Vector3.Distance(_rayHit.point, transform.position) > 1) {
                _aimDirection = (_rayHit.point - transform.position).normalized;
                _aimDirection.y = 0;

                transform.rotation = Quaternion.LookRotation(_aimDirection);
            }
        }
    }

    void ImpulsedInstantiate()
    {
        if (!primaryFire.IsPressed()) return;

        if (!_photonView.IsMine) return;

        GameObject projectile = PhotonNetwork.Instantiate(projectiles[0].projectilePrefab.name, transform.position, transform.rotation);

        if (projectile.GetPhotonView().IsMine) {
            Physics.IgnoreCollision(projectile.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
        }

        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.AddForce(_aimDirection * 20, ForceMode.Impulse);
    }
}
