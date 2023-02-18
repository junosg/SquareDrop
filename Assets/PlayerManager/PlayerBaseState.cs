using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void EnterState(PlayerManager playerManager);
    public abstract void UpdateState(PlayerManager playerManager);
    public abstract void FixedUpdateState(PlayerManager playerManager);
    public abstract void ExitState(PlayerManager playerManager);

    public abstract void CheckSwitchState(PlayerManager playerManager);
}
