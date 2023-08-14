using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "Player/PlayerData")]
public class PlayerSO : ScriptableObject
{
    public int playerID;
    public string playerName;
    public Color playerColor;
}
