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
    int ParseTextToInt(string text) //�ؽ�Ʈ�� ������ �Ľ�
    {
        if (string.IsNullOrEmpty(text)) //�ؽ�Ʈ ������� 0 ��ȯ
        {
            return 0;
        }

        text = text.Trim(); //��������

        string numericText = string.Concat(text.Where(c => char.IsDigit(c))); //���������� ���ڸ� ���

        if (int.TryParse(numericText, out int result))
        {
            return result;
        }
        else
        {
            Debug.LogError("�ȵ�. �ؽ�Ʈ: " + text);
            return 0;
        }

    }
}
