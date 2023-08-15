using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityController : Element
{
    [SerializeField] private List<Button> player1AbilityButtons;
    [SerializeField] private List<Button> player2AbilityButtons;

    private AbilityModel abilityModel;
    private PlayerModel targetModel;

    private List<int> indexesToClean = new List<int>();

    private void Start()
    {
        LinkAbilityButton(0);
        LinkAbilityButton(1);
    }

    public void SetModels(PlayerModel targetModel, AbilityModel abilityModel)
    {
        this.abilityModel = abilityModel;
        this.targetModel = targetModel;   
    }

    public void UseAbility(AbilityBase ability)
    {
        ability.Activate(targetModel);

        AvailableAbilities();

        abilityModel.abilitiesUsed.Add(ability);
        abilityModel.abilitiesActiveStatus.Add(ability, true);
        abilityModel.turnSinceAbilityUsed.Add(ability, 0);

        DeActivateAll();
    }

    public void DeActivateAll()
    {
        DeActivateAbilityButtons(0);
        DeActivateAbilityButtons(1);
    }

    public void DeActivateAbilityButtons(int abilitySlot)
    {
        player1AbilityButtons[abilitySlot].interactable = false;
        player2AbilityButtons[abilitySlot].interactable = false;
    }

    public void AvailableAbilities()
    {
        foreach (AbilityBase ability in abilityModel.Abilities) 
        { 
            if(!ability.CanExecute(targetModel)) 
            {
                DeActivateAbilityButtons(abilityModel.AbilityToButtonID[ability]);
            }
        }
    }

    public void ActivatePlayerButtons(int playerID)
    {
        if(playerID == 0)
        {
            player1AbilityButtons[0].interactable = true;
            player1AbilityButtons[1].interactable = true;
        }
        else if(playerID == 1) 
        {
            player2AbilityButtons[0].interactable = true;
            player2AbilityButtons[1].interactable = true;
        }
    }

    public void LinkAbilityButton(int abilitySlot)
    {
        player1AbilityButtons[abilitySlot].onClick.AddListener(() => UseAbility(abilityModel.ButtonIDToAbility[abilitySlot]));
        player2AbilityButtons[abilitySlot].onClick.AddListener(() => UseAbility(abilityModel.ButtonIDToAbility[abilitySlot]));
    }

    public void UpdateAbilityDurations()
    {
        for(int i = 0; i < abilityModel.abilitiesUsed.Count; i++)
        {
            if (abilityModel.abilitiesActiveStatus[abilityModel.abilitiesUsed[i]] == true)
            {
                if (abilityModel.turnSinceAbilityUsed[abilityModel.abilitiesUsed[i]] == abilityModel.abilitiesUsed[i].duration - 1)
                {
                    abilityModel.abilitiesUsed[i].DeActivate(targetModel);
                    indexesToClean.Add(i);
                }
                abilityModel.turnSinceAbilityUsed[abilityModel.abilitiesUsed[i]]++;
            }
        } 
    }

    public void UpdateUsedAbilities()
    {
        foreach(int i in indexesToClean)
        {
            abilityModel.abilitiesActiveStatus.Remove(abilityModel.abilitiesUsed[i]);
            abilityModel.turnSinceAbilityUsed.Remove(abilityModel.abilitiesUsed[i]);
            abilityModel.abilitiesUsed.RemoveAt(i);
        }
        indexesToClean.Clear();
    }
}
