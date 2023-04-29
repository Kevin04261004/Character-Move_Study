using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1 : NPC
{
    public Dialogue[] NPC1_Talk_0;
    private void Start()
    {
        NPC1_Talk_0 = gameObject.GetComponent<InteractionEvent>().GetDialogue();
    }
    private void Update()
    {
        if(isPlayerEnter && Input.GetKeyDown(KeyCode.F) && !reader.GetisReading())
        {
            GameManager.instance.playerMove.isTalkingTrue();
            reader.SetDialogue(NPC1_Talk_0,gameObject);
        }
        if(isReaded)
        {
            GameManager.instance.playerStats.DashOpen();
            isReaded = false;
            //�ι�°�� ������ ���� �ٲ�� �ϰ� ������ if�� �߰��ؼ� ����
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerEnter = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerEnter = false;
        GameManager.instance.playerMove.isTalkingFalse();
    }
}