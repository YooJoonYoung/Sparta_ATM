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

    public UserData(string name, int cash, int balance)
    {
        Name = name;
        Cash = cash;
        Balance = balance;
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
            //�Ա��Ҽ� ���±ݾ��Դϴ� UI
            Debug.Log("���ݺ���");
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
            //�ܾ׺��� UI
            Debug.Log("�ܾ� ����");
        }
    }
}