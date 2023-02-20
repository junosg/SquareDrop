using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Realtime;

public class PlayerActionManager : MonoBehaviour
{
    [Header("Stats")]

    [Range(0, 4)]
    [SerializeField] private float energy = 1;
    [SerializeField] private float energyRegenRatePerSecond = 1f;

    [Header("Projectiles")]
    [SerializeField] private ProjectileEntity[] projectiles;

    private InputManager _inputManager;
    private InputAction look;
    private InputAction primaryFire;
    private InputAction secondaryFire;

    private Ray _aimDirectionRay;
    private Vector3 _aimDirection;
    private RaycastHit _rayHit;
    private ProjectileEntity _currentProjectileEntity;

    private PhotonView _photonView;

    // Start is called before the first frame update
    void Awake()
    {
        _inputManager =  new InputManager();
        _photonView = gameObject.GetPhotonView();
        _currentProjectileEntity = projectiles[0];
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

    void Update()
    {
    }
    void FixedUpdate()
    {
        if (PhotonNetwork.IsConnected && !_photonView.IsMine) return;

        Aim();
        ImpulsedInstantiate();
        EnergyRegen();
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

    void EnergyRegen()
    {
        if (energy < 4)
            energy += energyRegenRatePerSecond * Time.deltaTime;
    }

    void ImpulsedInstantiate()
    {
        if (!primaryFire.IsPressed()) return;
        if (energy <= 0) return;

        GameObject projectile = null;

        if (PhotonNetwork.IsConnected) {
            projectile = PhotonNetwork.Instantiate(_currentProjectileEntity.projectilePrefab.name, transform.position, transform.rotation);
        } else {
            projectile = GameObject.Instantiate(_currentProjectileEntity.projectilePrefab, transform.position, transform.rotation);
        }

        energy -= _currentProjectileEntity.projectileCost;

        if (projectile.GetPhotonView().IsMine) {
            Physics.IgnoreCollision(projectile.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
        }

        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.AddForce(_aimDirection * 20, ForceMode.Impulse);

        StartCoroutine(ObjectDeath(_currentProjectileEntity.projectileLifetime, projectile));
    }

    IEnumerator ObjectDeath(float delay, GameObject dyingObject)
    {
        yield return new WaitForSeconds(delay);

        Destroy(dyingObject);
    }
}
