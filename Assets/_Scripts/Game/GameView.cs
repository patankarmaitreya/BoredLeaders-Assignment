using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameView : Element
{
    public PlayerView player1View;
    public PlayerView player2View;

    public AbilityView player1AbilitiesView;
    public AbilityView player2AbilitiesView;

    private void Awake()
    {
        player1View.SetModel(app.gameModel.player1Model);
        player2View.SetModel(app.gameModel.player2Model);

        player1AbilitiesView.SetModel(app.gameModel.player1AbilitiesModel);
        player2AbilitiesView.SetModel(app.gameModel.player2AbilitiesModel);
    }
}
