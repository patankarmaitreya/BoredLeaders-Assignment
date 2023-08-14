using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Backwards", menuName = "Abilities/Backward")]
public class Backwards : AbilityBase
{
    public override void Activate(PlayerModel target)
    {
        target.playerDir *= -1;
        target.roundCellNumber = true;
    }

    public override bool CanExecute(PlayerModel target)
    {
        if(target.state == PlayerState.FREE) 
        { 
            return true;
        }
        else 
        { 
            return false;
        }
    }

    public override void DeActivate(PlayerModel target)
    {
        target.playerDir *= -1;
        target.roundCellNumber = false;
    }
}
