using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum GameState
{
    Starting = 0,
    Player1Turn = 1,
    Player2Turn = 2,
    Ending = 3
}

public class GameController : Element
{
    public PlayerController playerController;
    public AbilityController abilityController;

    public Transform winText;
    private GameState State;
    private GameState lastState;

    private string winner;

    private void Awake()
    {
        playerController.onTurnFinished += SwitchStates;
    }

    void Start()
    {
        playerController.DeActivateAllButtons();
        abilityController.DeActivateAll();
        ChangeState(GameState.Starting);
    }
    public void ChangeState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.Starting:
                ChangeState(GameState.Player1Turn);
                break;
            case GameState.Player1Turn:
                HandleAbilities(app.gameModel.player1Model, app.gameModel.player2Model, app.gameModel.player1AbilitiesModel);
                HandlePlayerTurn(app.gameModel.player1Model);
                break;
            case GameState.Player2Turn:
                HandleAbilities(app.gameModel.player2Model, app.gameModel.player1Model, app.gameModel.player2AbilitiesModel);
                HandlePlayerTurn(app.gameModel.player2Model);
                break;
            case GameState.Ending:
                winText.GetChild(0).GetComponent<TextMeshProUGUI>().text = winner + " Wins";
                winText.gameObject.SetActive(true);
                break;
            default:
                Debug.LogError("Incorrect state passed as parameter");
                break;
        }

        //Debug.Log($"New state: {newState}");
    }

    public void HandleAbilities(PlayerModel playerModel, PlayerModel enemyModel, AbilityModel abilityModel)
    {
        abilityController.DeActivateAll();
        
        abilityController.SetModels(enemyModel, abilityModel);
        
        abilityController.UpdateAbilityDurations();
        abilityController.UpdateUsedAbilities();

        abilityController.ActivatePlayerButtons(playerModel.playerData.playerID);
        abilityController.AvailableAbilities();
    }

    public void HandlePlayerTurn(PlayerModel playerModel)
    {
        playerController.DeActivateAllButtons();
        playerController.SetModel(playerModel);
        playerController.ActivatePlayerButton(playerModel.playerData.playerID);
    }

    public void SwitchStates()
    {
        if(!HandleWin())
        {
            if (State == GameState.Player1Turn)
            {
                ChangeState(GameState.Player2Turn);
                lastState = GameState.Player2Turn;
            }
            else if (State == GameState.Player2Turn)
            {
                ChangeState(GameState.Player1Turn);
                lastState = GameState.Player1Turn;
            }
        }
        else
        {
            winner = (lastState == GameState.Player1Turn) ? "Player1" : "Player2";
            ChangeState(GameState.Ending);
        }
    }

    public bool HandleWin()
    {
        if(app.gameModel.player1Model.cellNumber == 9 ||
            app.gameModel.player2Model.cellNumber == 0)
        {
            return true;
        }
        else { return false; }
    }
}
