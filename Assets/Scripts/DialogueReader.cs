using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueReader : MonoBehaviour
{
    private NPC npc;
    private Dialogue[] dialogues;
    private bool isReading;
    private float time;
    [SerializeField] private float typingtime = 0.02f;
    private int wordIndex;
    private int typeIndex = 0;
    private int ContextsIndex = 0;
    [SerializeField] private bool isTyping = false;
    public void SetDialogue(Dialogue[] _Set, GameObject parent)
    {
        npc = parent.GetComponent<NPC>();
        dialogues = _Set;
        StartReading();
    }
    public void Update()
    {
        if(isReading && Input.GetKeyDown(KeyCode.Return))
        {
            if(isTyping)
            {
                isTyping = false;
                wordIndex = 0;

                GameManager.instance.ui.TalkPanel_Change(dialogues[typeIndex].name, dialogues[typeIndex].contexts[ContextsIndex]);
            }
            else
            {
                if (typeIndex == dialogues.Length)
                {
                    isTyping = false;
                    isReading = false;
                    GameManager.instance.playerMove.isTalkingFalse();
                    GameManager.instance.ui.TalkPanel_Close();
                    npc.isReadedTrue();
                    typeIndex = 0;
                }
                else
                {
                    isTyping = true;
                    if (dialogues[typeIndex].contexts.Length > ContextsIndex+1)
                    {
                        ContextsIndex++;
                    }
                    else
                    {
                        if (typeIndex+1 == dialogues.Length)
                        {
                            isTyping = false;
                            isReading = false;
                            GameManager.instance.playerMove.isTalkingFalse();
                            GameManager.instance.ui.TalkPanel_Close();
                            npc.isReadedTrue();
                            typeIndex = 0;
                        }
                        typeIndex++;
                        ContextsIndex = 0;
                    }
                }
            }
        }
    }
    public void FixedUpdate()
    {
        if(isTyping)
        {
            time += Time.deltaTime;
            if (time > typingtime)
            {
                time = 0;
                wordIndex++;
                if (wordIndex > dialogues[typeIndex].contexts[ContextsIndex].Length)
                {
                    isTyping = false;
                    wordIndex = 0;
                }
                else
                {
                    GameManager.instance.ui.TalkPanel_Change_WithTyping(dialogues[typeIndex].name, dialogues[typeIndex].contexts[ContextsIndex], wordIndex);
                }
            }
        }
    }
    public bool GetisReading()
    {
        return isReading;
    }
    public void StartReading()
    {
        isReading = true;
        isTyping = true;
    }
    
}
