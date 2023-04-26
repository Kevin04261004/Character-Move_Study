using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] protected bool isOpened;
    [SerializeField] protected int ContentIndex = -1;
    [SerializeField] protected GameObject Target;
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision)
        {
            if (collision.CompareTag("Player"))
            {
                isOpened = true;
            }
        }
    }
    protected void OnTriggerExit2D(Collider2D collision)
    {
        if(collision)
        {
            if(collision.CompareTag("Player"))
            {
                Target = collision.gameObject;
                isOpened = false;
                GameManager.instance.ui.TalkPanel_Close();
                ContentIndex = -1;
            }
        }
    }
}
