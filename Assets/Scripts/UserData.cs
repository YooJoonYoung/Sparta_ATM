using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

[System.Serializable]
public class UserData 
{
    public string Name;
    public int Cash;
    public int Balance;
    public string ID;
    public string PS; 
    public UserData(string name, int cash, int balance, string id, string ps)
    {
        Name = name;
        Cash = cash;
        Balance = balance;
        ID = id;
        PS = ps;
    }
   public void AddMoney(int amount)
    {
    

        if (Cash >= amount)
        {
            Cash -= amount;
            Balance += amount;


        }
        else
        {
            GameManager.Instance.popUpError.SetActive(true);           
        }
    }
    public void SubtractMoney(int amount)
    {
        

        if (Balance >= amount)
        {
            Balance -= amount;
            Cash += amount;
        }
        else
        {
            GameManager.Instance.popUpError.SetActive(true);
        }
    }
    public bool CheckCredentials(string id, string ps) //Id,PS 확인하는 매서드 추가
    {
        return ID == id && PS == ps;
    }
}