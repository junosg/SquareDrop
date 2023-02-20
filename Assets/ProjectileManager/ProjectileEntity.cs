using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectileEntity
{
    public string projectileName = "ProjectileName";
    public GameObject projectilePrefab;
    public float projectileEmitDelay = 0f;
    public float projectileEmitForce = 4f;
    public float projectileLifetime = 2f;
    public float projectileCost = 1f;
}
