using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour, IHitable
{
    [Range(0, 100)]
    public int Health;
    public int MaxHealth = 100;
    public Slider HealthSliderUI;
    
    // 아이템 프리팹들
    public GameObject HealthItemPrefab;
    public GameObject StaminaItemPrefab;
    public GameObject BulletItemPrefab;
    

    public void Hit(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {   
        // 죽을때 아이템 생성
        // 실습 과제 31. 몬스터가 죽으면 아이템이 드랍
        // (Health: 20%, Stamina: 20%, Bullet:10%)
        GameObject itemObject = null;
        int percentage = UnityEngine.Random.Range(0, 100);
        if (percentage <= 20) // 20%
        {
            itemObject = Instantiate(HealthItemPrefab);
        }
        else if (percentage <= 40)
        {
            itemObject = Instantiate(StaminaItemPrefab);
        }
        else if(percentage <= 50)
        {
            itemObject = Instantiate(BulletItemPrefab);
        }

        if (itemObject != null)
        {
            itemObject.transform.position = this.transform.position;
        }

        Destroy(gameObject);
    }
    
    
    private void Update()
    {
        HealthSliderUI.value = (float)Health / (float)MaxHealth;  // 0 ~ 1
    }
    
    private void Start()
    {
        Init();
    }
    
    public void Init()
    {
        Health = MaxHealth;
    }
    
    

    
}
