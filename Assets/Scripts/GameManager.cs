using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public UserData userdata;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI balanceText;

    // Start is called before the first frame update

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject); 
        }
    }

    void Start()
    {
        userdata = new UserData("유준영", 100000, 50000);
        Refresh(); //초기 UI 업데이트
    }

    public void Refresh()
    {
        // UI에 이름, 현금, 잔액을 업데이트
        nameText.text = userdata.Name;
        cashText.text = string.Format("{0:N0}", userdata.Cash);  // 천 단위 콤마
        balanceText.text = string.Format("{0:N0}", userdata.Balance);  // 천 단위 콤마
    }

    // 데이터를 갱신할 메서드 (예시로 사용자가 현금을 변경할 때)
    public void UpdateUserData(string name, int cash, int balance)
    {
        userdata.Name = name;
        userdata.Cash = cash;
        userdata.Balance = balance;
        Refresh();  // 데이터가 변경되었으면 UI 갱신
    }

    public void Add(int amount)
    {
        userdata.AddMoney(amount);
        Refresh();
    }
    public void Sub(int amount)
    {
        userdata.SubtractMoney(amount);
        Refresh();
    }
}