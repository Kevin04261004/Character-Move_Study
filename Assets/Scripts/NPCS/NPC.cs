using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] protected DialogueReader reader;
    [SerializeField] protected bool isPlayerEnter = false;
    [SerializeField] protected bool isReaded;

    public virtual void isReadedTrue()
    {
        isReaded = true;
    }
}
