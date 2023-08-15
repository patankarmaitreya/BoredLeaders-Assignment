using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using Random = UnityEngine.Random;

public class PlayerController : Element
{
    [SerializeField] private List<Button> playerDicecastButtons;

    [SerializeField] private Transform gridHolder;

    private PlayerModel model;

    public event Action onPositionUpdated;
    public event Action onTurnFinished;

    public void SetModel(PlayerModel model)
    {
        this.model = model;
    }

    private void Start()
    {
        LinkDiceButtons();
    }


    public void UpdatePosition()
    {
        if(model.state == PlayerState.FREE) 
        {
            int rollResult = RollDice(6);
            Debug.Log("Rol Result - " + rollResult);
            int newPos = GetNewPosition(rollResult);

            model.cellNumber = newPos;
            if (onPositionUpdated != null)
            {
                onPositionUpdated();
            }
        }
        if (onTurnFinished != null)
        {
            onTurnFinished();
        }
    }

    private int RollDice(int dicefaces)
    {
        var rollResult = Random.Range(1, dicefaces + 1);
        return rollResult;
    }

    private int GetNewPosition(int rollResult)
    {
        int newPos = model.cellNumber + rollResult * (int)model.playerDir;
        if (newPos <= gridHolder.childCount - 1 && newPos >= 0)
        {
            return newPos;
        }
        else
        {
            if (model.roundCellNumber)
            {
                return (newPos < 0) ? 0 : gridHolder.childCount - 1;
            }
            else 
            {
                Debug.Log(model.playerData.playerName + " new position outside board");
                return model.cellNumber;
            }
        }
    }

    public void LinkDiceButtons()
    {
        foreach(Button button in playerDicecastButtons)
        {
            button.onClick.AddListener(() => UpdatePosition());
        }
    }

    public void DeActivateAllButtons()
    {
        foreach (Button button in playerDicecastButtons)
        {
            button.interactable = false;
        }
    }

    public void ActivatePlayerButton(int playerID)
    {
        playerDicecastButtons[playerID].interactable = true;
    }
}
