using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button updateButton;  // 데이터 업데이트 버튼

    void Start()
    {  
        updateButton.onClick.AddListener(OnUpdateButtonClicked);// 버튼 클릭 이벤트 리스너 등록
    }

    // 버튼 클릭 시 호출될 메서드
    void OnUpdateButtonClicked()
    {
        // UserData를 새로운 값으로 업데이트하고 UI 갱신
        GameManager.Instance.UpdateUserData("새로운 이름", 200000, 100000);
    }
}
