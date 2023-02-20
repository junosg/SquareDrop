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
        playerManager.rigidBody.MoveRotation(Quaternion.Lerp(playerManager.rigidBody.rotation, Quaternion.LookRotation(new Vector3(playerManager.move.ReadValue<Vector2>().x, 0, playerManager.move.ReadValue<Vector2>().y)), .2f));

        playerManager.cameraFollowTarget.transform.position = Vector3.Lerp(playerManager.cameraFollowTarget.transform.position, playerManager.transform.position + new Vector3(0, 10, -.5f), .2f) ;
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
