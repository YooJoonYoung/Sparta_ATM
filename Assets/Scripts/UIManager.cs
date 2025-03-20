using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button updateButton;  // ������ ������Ʈ ��ư

    void Start()
    {  
        updateButton.onClick.AddListener(OnUpdateButtonClicked);// ��ư Ŭ�� �̺�Ʈ ������ ���
    }

    // ��ư Ŭ�� �� ȣ��� �޼���
    void OnUpdateButtonClicked()
    {
        // UserData�� ���ο� ������ ������Ʈ�ϰ� UI ����
        GameManager.Instance.UpdateUserData("���ο� �̸�", 200000, 100000);
    }
}
