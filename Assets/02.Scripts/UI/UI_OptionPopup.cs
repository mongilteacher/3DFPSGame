using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_OptionPopup : MonoBehaviour
{
    public void Open()
    {
        // 사운드 효과음이라던지
        // 초기화 함수
        gameObject.SetActive(true);
    }

    public void Close()
    {
        // 사운드 효과음이라던지...
        // 여러 가지
        gameObject.SetActive(false);
    }
    
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    
    public void OnClickContinueButton()
    {
        Debug.Log("계속하기");
        
        GameManager.Instance.Continue();
        
        Close();
    }

    public void OnClickResumeButton()
    {
        Debug.Log("다시하기");
    }

    public void OnClickExitButton()
    {
        Debug.Log("게임종료");
    }
}
