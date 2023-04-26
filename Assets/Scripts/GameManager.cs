using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public TalkManager talkManager;
    public UIManager ui;
    public PlayerStats playerStats;
}
