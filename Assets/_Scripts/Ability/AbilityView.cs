using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityView : MonoBehaviour
{
    private AbilityModel model;

    void Start()
    {
        transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = model.Abilities[0].abilityName;
        transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = model.Abilities[0].abilityDescription;
        
        transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = model.Abilities[1].abilityName;
        transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = model.Abilities[1].abilityDescription;
    }

    public void SetModel(AbilityModel model)
    {
        this.model = model;
    }
}
