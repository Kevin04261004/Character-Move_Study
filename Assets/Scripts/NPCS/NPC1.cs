using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPC1_Talk_Data
{
    public TalkData[] NPC1_Talk_0;
}
public class NPC1 : NPC
{
    public TextAsset data;
    public NPC1_Talk_Data datas;
    private void Awake()
    {
        datas = JsonUtility.FromJson<NPC1_Talk_Data>(data.text);
    }
    private void Update()
    {
        if (isOpened)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ContentIndex++;
                if (ContentIndex >= datas.NPC1_Talk_0.Length)
                {
                    ContentIndex = -1;
                    GameManager.instance.playerStats.DashOpen();
                    GameManager.instance.ui.TalkPanel_Close();
                }
                else
                {
                    GameManager.instance.ui.TalkPanel_Change(datas.NPC1_Talk_0[ContentIndex].name, datas.NPC1_Talk_0[ContentIndex].content);
                }
            }
        }
    }
}
