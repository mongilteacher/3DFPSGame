using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MonsterState // 몬스터의 상태
{
    Idle,           // 대기
    Trace,          // 추적
    Attack,         // 공격
    Return,         // 복귀
    Damaged,        // 공격 당함
    Die             // 사망
}

public class Monster : MonoBehaviour, IHitable
{
    [Range(0, 100)]
    public int Health;
    public int MaxHealth = 100;
    public Slider HealthSliderUI;
    /***********************************************************************/
    
    private CharacterController _characterController;

    private Transform _target;       // 플레이어
    public float FindDistance   = 5f;  // 감지 거리
    public float AttackDistance = 2f;  // 공격 범위 

    private MonsterState _currentState = MonsterState.Idle;
    
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        
        Init();
    }
    
    public void Init()
    {
        Health = MaxHealth;
    }
    
    private void Update()
    {
        HealthSliderUI.value = (float)Health / (float)MaxHealth;  // 0 ~ 1
        
        // 상태 패턴: 상태에 따라 행동을 다르게 하는 패턴 
        // 1. 몬스터가 가질 수 있는 행동에 따라 상태를 나눈다.
        // 2. 상태들이 조건에 따라 자연스럽게 전환(Transition)되게 설계한다.

        switch (_currentState)
        {
            case MonsterState.Idle:
                Idle();
                break;
            
            case MonsterState.Trace:
                Trace();
                break;
        }
    }

    private void Idle()
    {
        // todo: 몬스터의 Idle 애니메이션 재생
        if(Vector3.Distance(_target.position, transform.position) <= FindDistance)
        {
            Debug.Log("상태 전환: Idle -> Trace");
            _currentState = MonsterState.Trace;
        }
    }

    private void Trace()
    { 
        // Trace 상태일때의 행동 코드를 작성
        
        // 플레이어게 다가간다.


    }
    
    
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
        ItemObjectFactory.Instance.MakePercent(transform.position);        

        Destroy(gameObject);
    }
    
    
    
    
   
    
    

    
}
