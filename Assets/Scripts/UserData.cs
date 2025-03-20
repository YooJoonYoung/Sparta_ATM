using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public TextMeshProUGUI curBalance;
    public TextMeshProUGUI curCash;

    public int Balance = 500000;
    public int Cash = 100000;
    // Start is called before the first frame update
    void Start()
    {

        UpdateUI();
    }

    void UpdateUI()
    {
        curBalance.text = string.Format("{0:N0}", Balance);
        curCash.text = string.Format("{0:N0}",Cash);

    }
}