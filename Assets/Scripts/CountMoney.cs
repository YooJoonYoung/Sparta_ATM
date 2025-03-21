using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountMoney : MonoBehaviour
{
   public Button button10D;
   public Button button30D;
   public Button button50D;
   public Button buttonCustoomD;

   public Button button10W;
   public Button button30W;
   public Button button50W;
   public Button buttonCustroomW;

    //UI Text ������Ʈ�� ����� ����
    public TextMeshProUGUI userwalletText;  // ����� ������ ǥ���� Text
    public TextMeshProUGUI balanceText;     // ����� �ܾ��� ǥ���� Text

    public TMP_InputField inputFieldW;
    public TMP_InputField inputFieldD;


    private void Start()
    {
        button10D.onClick.AddListener(() => AddMoney(10000));
        button30D.onClick.AddListener(() => AddMoney(30000));
        button50D.onClick.AddListener(() => AddMoney(50000));
        buttonCustoomD.onClick.AddListener(() => AddCustomMoney(isDeposit: true,int.Parse(inputFieldD.text) ));

        button10W.onClick.AddListener(() => SubtractMoney(10000));
        button30W.onClick.AddListener(() => SubtractMoney(30000));
        button50W.onClick.AddListener(() => SubtractMoney(50000));
        buttonCustroomW.onClick.AddListener(() => AddCustomMoney(isDeposit: false, int.Parse(inputFieldW.text)));

        //UpdateBalance();
    }

    void AddMoney(int amount)
    {
       GameManager.Instance.Add(amount);

    }

    void SubtractMoney(int amount)
    {
       GameManager.Instance.Sub(amount);
    }

    void AddCustomMoney(bool isDeposit,int customAmount) //�Ұ����� ����� �ѹ��� ����, ���ϴ±ݾ�
    {

        if (isDeposit) //�Ա�
        {
            AddMoney(customAmount);
        }

        else //���
        {
            SubtractMoney(customAmount);
        }
    }
    //int GetUserwalletAmount()
    //{
    //    return int.Parse(userwalletText.text); // Text���� ���� �κ��� ������ int�� ��ȯ
    //}
    //int GetBalanceAmount()
    //{
    //    return int.Parse(balanceText.text); // Text���� ���� �κ��� ������ int�� ��ȯ
    //}

    //void SetUserwalletAmount(int amount) // userwallet �ݾ��� Text�� ����
    //{
    //    userwalletText.text = amount.ToString();  
    //}

   
    //void SetBalanceAmount(int amount) // balance �ݾ��� Text�� ����
    //{
    //    balanceText.text = amount.ToString();
    //}

    //void UpdateBalance()  // userwallet�� balance ���� ����� ������ UI�� ����
    //{
       
    //    SetUserwalletAmount(GetUserwalletAmount());
    //    SetBalanceAmount(GetBalanceAmount());
    //}
}
