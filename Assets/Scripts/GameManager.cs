using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public UIManager ui;
    public PlayerMove playerMove;
    public PlayerStats playerStats;
    public DialogueParser dialogueParser;
}
