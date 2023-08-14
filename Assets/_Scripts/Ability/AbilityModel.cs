using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class AbilityModel : Element
{
    public List<AbilityBase> Abilities;
    public List<AbilityBase> abilitiesUsed = new List<AbilityBase>();
    public Dictionary<AbilityBase, bool> abilitiesActiveStatus = new Dictionary<AbilityBase, bool>();
    public Dictionary<AbilityBase, int > turnSinceAbilityUsed = new Dictionary<AbilityBase, int>();

    public Dictionary<int, AbilityBase> ButtonIDToAbility = new Dictionary<int, AbilityBase>();
    public Dictionary<AbilityBase, int> AbilityToButtonID = new Dictionary<AbilityBase, int>();

    private void Start()
    {
        for(int i = 0; i < Abilities.Count; i++) 
        {
            ButtonIDToAbility.Add(i, Abilities[i]);
            AbilityToButtonID.Add(Abilities[i], i);
        }
    }
}
