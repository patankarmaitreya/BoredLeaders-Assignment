using UnityEngine;

public abstract class AbilityBase : ScriptableObject
{
    public string abilityName;
    public string abilityDescription;
    public int duration; //since board game Duration will be number of moves hence int

    public abstract bool CanExecute(PlayerModel target);
    public abstract void Activate(PlayerModel target);
    public abstract void DeActivate(PlayerModel target);
}
