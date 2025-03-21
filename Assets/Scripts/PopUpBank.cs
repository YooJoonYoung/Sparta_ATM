using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpBank : MonoBehaviour
{
    public TextMeshProUGUI curBalance;
    public TextMeshProUGUI curCash;
    public Button DepositButton;
    public Button WithdrawalButton;
    public GameObject Deposit;
    public GameObject Withdrawal;
    public Button BackBtn1;
    public Button BackBtn2;

    // Start is called before the first frame update
    void Start()
    {
        Withdrawal.SetActive(false);
        Deposit.SetActive(false);
        DepositButton.onClick.AddListener(OnDePositButtonClicked);
        WithdrawalButton.onClick.AddListener(OnWithdrawalButtonClicked);
        BackBtn1.onClick.AddListener(OnBackBtnClicked);
        BackBtn2.onClick.AddListener(OnBackBtnClicked);
    
    }
    void OnDePositButtonClicked()
    {
        DepositButton.gameObject.SetActive(false);
        WithdrawalButton.gameObject.SetActive(false);
        Deposit.SetActive(true);
        Withdrawal.SetActive(false);
    }
    void OnWithdrawalButtonClicked()
    {
        DepositButton.gameObject.SetActive(false);
        WithdrawalButton.gameObject.SetActive(false);
        Withdrawal.SetActive(true);
        Deposit.SetActive(false);
    }
    void OnBackBtnClicked()
    {
        Withdrawal.SetActive(false);
        Deposit.SetActive(false);
        DepositButton.gameObject.SetActive(true);
        WithdrawalButton.gameObject.SetActive(true);
    }

}
