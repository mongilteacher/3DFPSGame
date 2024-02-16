using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerGunFire : MonoBehaviour
{
    // 목표: 마우스 왼쪽 버튼을 누르면 시선이 바라보는 방향으로 총을 발사하고 싶다.
    // 필요 속성
    // - 총알 튀는 이펙트 프리팹
    public ParticleSystem HitEffect;

    private const int BulletHouseCount = 10;
    private const int BulletCountPerHouse = 30;
    
    public int BulletTotalRemainCount;
    public int BulletRemainCount;
    public int BulletMaxCount = 30;
    private float _fireTimer;
    public float FIRE_TIME = 0.2f;

    public Text BulletTextUI;
    
    private void Start()
    {
        BulletTotalRemainCount = BulletHouseCount * BulletCountPerHouse;
        BulletRemainCount = BulletCountPerHouse;
        
        
        
        RefreshUI();
    }

    private void RefreshUI()
    {
        BulletTextUI.text = $"{BulletRemainCount:D2}/{BulletTotalRemainCount:D2}";
    }

    private void Update()
    {
        if (BulletTotalRemainCount > 0 && BulletRemainCount < BulletCountPerHouse && Input.GetKeyDown(KeyCode.R))
        {
            int diff = BulletCountPerHouse - BulletRemainCount;
            diff = Math.Min(BulletTotalRemainCount, diff);
            
            BulletTotalRemainCount -= diff;
            BulletRemainCount += diff;
            
            RefreshUI();
        }

        _fireTimer -= Time.deltaTime;
        
        // 1. 만약에 마우스 왼쪽 버튼을 누르면
        if (BulletRemainCount > 0 && _fireTimer <= 0 && Input.GetMouseButton(0))
        {
            
            CameraManager.Instance.Shake();
            
            _fireTimer = FIRE_TIME;
            BulletRemainCount--;
            RefreshUI();

            // 2. 레이(광선)을 생성하고, 위치와 방향을 설정한다.
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            // 3. 레이를 발사한다.
            // 4. 레이가 부딛힌 대상의 정보를 받아온다.
            RaycastHit hitInfo;
            bool IsHit = Physics.Raycast(ray, out hitInfo);
            if (IsHit)
            {
                // 5. 부딛힌 위치에 (총알이 튀는)이펙트를 위치한다.
                HitEffect.gameObject.transform.position = hitInfo.point;
                // 6. 이펙트가 쳐다보는 방향을 부딛힌 위치의 법선 벡터로 한다.
                HitEffect.gameObject.transform.forward  = hitInfo.normal; 
                HitEffect.Play();
            }
        }
        
    }
}
