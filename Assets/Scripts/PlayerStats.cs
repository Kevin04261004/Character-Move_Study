using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 5.0f;
    [SerializeField] private float jumpPower = 8f;
    [SerializeField] private float dashPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    [SerializeField] private bool isDoubleJumpOpened = false;
    [SerializeField] private bool isDashOpened = false;
    public float GetMoveSpeed()
    {
        return MoveSpeed;
    }
    public float GetJumpPower()
    {
        return jumpPower;
    }
    public float GetdashPower()
    {
        return dashPower;
    }
    public float GetdashingTime()
    {
        return dashingTime;
    }
    public float GetdashingCooldown()
    {
        return dashingCooldown;
    }
    public bool GetisDoubleJumpOpened()
    {
        return isDoubleJumpOpened;
    }    
    public bool GetisDashOpened()
    {
        return isDashOpened;
    }
    public void DoubleJumpOpen()
    {
        isDoubleJumpOpened = true;
    }
    public void DashOpen()
    {
        isDashOpened = true;
        gameObject.GetComponent<PlayerMove>().GetDashImage().gameObject.SetActive(true);
    }
}
