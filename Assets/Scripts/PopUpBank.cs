using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PopUpBank : MonoBehaviour
{
    public TextMeshProUGUI curBalance;
    public TextMeshProUGUI curCash;

    // Start is called before the first frame update
    void Start()
    {

        UpdateUI();
    }

    void UpdateUI()
    {
        int balance = ParseTextToInt(curBalance.text);
        int cash = ParseTextToInt(curCash.text);

        curBalance.text = string.Format("{0:N0}", balance);
          curCash.text = string.Format("{0:N0}", cash);
    }
    int ParseTextToInt(string text) //텍스트를 정수로 파싱
    {
        if (string.IsNullOrEmpty(text)) //텍스트 비었으면 0 반환
        {
            return 0;
        }

        text = text.Trim(); //공백제거

        string numericText = string.Concat(text.Where(c => char.IsDigit(c))); //문자있으면 숫자만 출력

        if (int.TryParse(numericText, out int result))
        {
            return result;
        }
        else
        {
            Debug.LogError("안됨. 텍스트: " + text);
            return 0;
        }

    }
}
