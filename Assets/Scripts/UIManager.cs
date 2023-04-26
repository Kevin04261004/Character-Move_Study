using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image TalkPanel_Image;
    [SerializeField] private TextMeshProUGUI TalkPanel_Name_TMP;
    [SerializeField] private TextMeshProUGUI TalkPanel_Content_TMP;
    [SerializeField] private Image dashSkill;
    public Image GetDashImage()
    {
        return dashSkill;
    }
    public void TalkPanel_Change(string name, string content = null)
    {
        TalkPanel_Image.gameObject.SetActive(true);
        if (content == null)
        {
            TalkPanel_Image.gameObject.SetActive(false);
        }
        else
        {
            TalkPanel_Name_TMP.text = name;
            TalkPanel_Content_TMP.text = content;
        }
    }
    public void TalkPanel_Close()
    {
        TalkPanel_Image.gameObject.SetActive(false);
    }

}
