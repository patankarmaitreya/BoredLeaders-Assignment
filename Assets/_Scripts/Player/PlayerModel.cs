using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public enum PlayerState
{
    FREE = 0,
    RESTRAINED = 1,
}


public class PlayerModel : Element
{
    [SerializeField]private Transform gridHolder;

    public PlayerSO playerData;
    public PlayerState state;

    public int cellNumber;
    
    public float playerDir;
    public bool roundCellNumber;

    public GameObject cageHolder;

    public event Action onPositionChange;

    private void Awake()
    {
        SetStartingCells();
        state = PlayerState.FREE;
        playerDir = Mathf.Sign(gridHolder.childCount / 2 - cellNumber);
    }
    private void Start()
    {
        app.gameController.playerController.onPositionUpdated += CheckPositionUpdate;
    }

    public void CheckPositionUpdate()
    {    
        if (onPositionChange != null)
        {
            onPositionChange();
        }
    }

    public void SetStartingCells()
    {
        switch(playerData.playerID)
        {
            case 0:
                cellNumber = 0;
                break;
            case 1:
                cellNumber = gridHolder.childCount - 1;
                break;
            default:
                Debug.LogError("No Starting Cell found for playerID: " + playerData.playerID);
                break; 
        }
    }

    public Vector3 GetPlayerPosition(int cellNumber)
    {
        return gridHolder.GetChild(cellNumber).position;
    }
}
