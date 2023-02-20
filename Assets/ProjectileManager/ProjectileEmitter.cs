using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Realtime;

public class ProjectileEmitter : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileEmitForce;
    [SerializeField] private float projectileEmitDelay;
    [SerializeField] private float projectileLifetime;
    [SerializeField] private float projectileRefreshTime;
    [SerializeField] private int projectileAmount;

    private InputManager _inputManager;

    private InputAction look;
    private InputAction fire;

    private Ray _aimDirectionRay;
    private Vector3 _aimDirection;
    private RaycastHit _rayHit;
    private PhotonView photonView;

    void OnEnable()
    {
        look = _inputManager.Player.Look;
        look.Enable();

        fire = _inputManager.Player.Fire;
        fire.Enable();
    }

    void Awake()
    {
        _inputManager = new InputManager();
        photonView = gameObject.transform.parent.transform.parent.GetComponent<PhotonView>();
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine) return;

        Aim();
        Fire();
    }

    void Fire()
    {
        if (fire.IsPressed()) {
            StartCoroutine(EmitProjectile(projectile, projectileEmitDelay, projectileEmitForce, projectileLifetime));
            Debug.Log("Pressed");
        }
    }

    void Aim()
    {
        _aimDirectionRay = Camera.main.ScreenPointToRay(look.ReadValue<Vector2>());

        if (Physics.Raycast(_aimDirectionRay, out _rayHit, 100, ~LayerMask.NameToLayer("Ground"))) {
            _aimDirection = (_rayHit.point - transform.position).normalized;
            transform.parent.rotation = Quaternion.LookRotation(_aimDirection);
        }
    }

    IEnumerator EmitProjectile(GameObject projectile, float delay, float projectileEmitForce, float projectileLifetime)
    {
        yield return new WaitForSeconds(delay);

        GameObject emitted = PhotonNetwork.Instantiate(projectile.name, transform.position, Quaternion.LookRotation(transform.parent.forward));
        Rigidbody projectileRigidbody = emitted.GetComponent<Rigidbody>();
        projectileRigidbody.AddForce(transform.parent.forward * projectileEmitForce, ForceMode.Impulse);

        yield return new WaitForSeconds(projectileLifetime);

        Destroy(emitted);
    }
}
