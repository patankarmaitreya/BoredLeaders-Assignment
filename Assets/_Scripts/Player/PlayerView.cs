using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : Element
{
    private PlayerModel model;

    private PlayerView (PlayerModel model)
    {
        this.model = model;
    }

    private void Start()
    {
        model.onPositionChange += Move;
    }

    public void SetModel(PlayerModel model)
    {
        this.model = model;
    }

    public void Move()
    {
        transform.position = model.GetPlayerPosition(model.cellNumber);
    }
}
