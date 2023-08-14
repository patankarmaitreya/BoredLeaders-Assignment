using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Imprision", menuName = "Abilities/Imprision")]
public class Imprision : AbilityBase
{
    public GameObject cagePrefab;

    public override void Activate(PlayerModel target)
    {
        target.state = PlayerState.RESTRAINED;
        target.cageHolder = Instantiate(cagePrefab, target.GetPlayerPosition(target.cellNumber), Quaternion.identity);
    }

    public override void DeActivate(PlayerModel target)
    {
        target.state = PlayerState.FREE;
        Destroy(target.cageHolder);
    }

    public override bool CanExecute(PlayerModel target)
    {
        if (target.state == PlayerState.FREE)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
