using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Abilities
{
    Imprision = 0,
    Backwards = 1
}

[CreateAssetMenu(fileName = "New PlayerData", menuName = "Player/PlayerData")]
public class PlayerSO : ScriptableObject
{
    public int playerID;
    public string playerName;
    public Color playerColor;
    public List<Abilities> playerAbilities;
}
