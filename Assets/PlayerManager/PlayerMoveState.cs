using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public override void EnterState(PlayerManager playerManager)
    {
    }

    public override void UpdateState(PlayerManager playerManager)
    {
    }

    public override void FixedUpdateState(PlayerManager playerManager)
    {
        playerManager.rigidBody.velocity = new Vector3(playerManager.move.ReadValue<Vector2>().x, 0, playerManager.move.ReadValue<Vector2>().y) * playerManager.movespeed;
    }

    public override void ExitState(PlayerManager playerManager)
    {
    }

    public override void CheckSwitchState(PlayerManager playerManager)
    {
        if (playerManager.move.ReadValue<Vector2>() == Vector2.zero) {
            playerManager.SwitchState(playerManager.idleState);
        }
    }
}
