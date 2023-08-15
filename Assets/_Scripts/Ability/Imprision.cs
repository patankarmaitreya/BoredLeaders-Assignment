using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Imprision", menuName = "Abilities/Imprision")]
public class Imprision : AbilityBase
{
    public GameObject cagePrefab;
    public Animator animator;

    public override void Activate(PlayerModel target)
    {
        target.state = PlayerState.RESTRAINED;
        target.cageHolder = Instantiate(cagePrefab, target.GetPlayerPosition(target.cellNumber), Quaternion.identity);
        target.cageHolder.GetComponent<Animator>().SetTrigger("Spawn");
    }

    public override void DeActivate(PlayerModel target)
    {
        target.state = PlayerState.FREE;
        target.cageHolder.GetComponent<Animator>().SetTrigger("Destroy");
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
