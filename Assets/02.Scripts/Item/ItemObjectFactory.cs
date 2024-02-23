using System.Collections.Generic;
using UnityEngine;

// 아이템 공장의 역할: 아이템 오브젝트의 생성을 책임진다.
// **팩토리 패턴**
// 객체 생성을 공장 클래스를 이용해 캡슐화 처리하여 대신 "생성"하게 하는 디자인 패턴
// 객체 생성에 필요한 과정을 템플릿화 해놓고 외부에서 쉽게 사용한다.
// 장점:
// 1. 생성과 처리 로직을 분리하여 결합도를 낮출 수 있다. (결합도: 참조를 통해 상호 의존성의 높아지는 정도)
// 2. 확장 및 유지보수가 편리하다.
// 3. 객체 생성 후 공통으로 할 일을 수행하도록 지정해줄 수 있다.
// 단점:
// 1. 상대적으로 쪼끔 더 복잡하다.
// 2. 그래서 공부해야 한다.
// 3. 그거 말고는 단점이 크게 없다.
public class ItemObjectFactory : MonoBehaviour
{
    public static ItemObjectFactory Instance { get; private set; }
    
    // (생성할)아이템 프리팹들
    public List<GameObject> ItemPrefabs;

    // 공장의 창고
    private List<ItemObject> _itemPool;
    public int PoolCount = 10;
    
    private void Awake()
    {
        Instance = this;
        
        _itemPool = new List<ItemObject>();

        for (int i = 0; i < PoolCount; ++i)            // 10번
        {
            foreach (GameObject prefab in ItemPrefabs) // 3개
            {
                // 1. 만들고
                GameObject item = Instantiate(prefab);
                // 2. 창고에 넣는다.
                item.transform.SetParent(this.transform);
                _itemPool.Add(item.GetComponent<ItemObject>());
                // 3. 비활성화
                item.SetActive(false);
            }
        }
    }

    private ItemObject Get(ItemType itemType) // 창고 뒤지기
    {
        foreach (ItemObject itemObject in _itemPool) // 창고를 뒤진다.
        {
            if (itemObject.gameObject.activeSelf == false
                && itemObject.ItemType == itemType)
            {
                return itemObject;
            }
        }

        return null;
    }
    
    
    // 확률 생성 (공장아! 랜덤박스 주문할게!)
    public void MakePercent(Vector3 position)
    {
        int percentage = UnityEngine.Random.Range(0, 100);
        if (percentage <= 20) // 20%
        {
            Make(ItemType.Health, position);
        }
        else if (percentage <= 40)
        {
            Make(ItemType.Stamina, position);
        }
        else if(percentage <= 50)
        {
            Make(ItemType.Bullet, position);
        }
    }
    
    // 기본 생성 (공장아! 내가 원하는거 주문할게!)
    public void Make(ItemType itemType, Vector3 position)
    {
        ItemObject itemObject = Get(itemType);

        if (itemObject != null)
        {
            itemObject.transform.position = position;
            itemObject.Init();
            itemObject.gameObject.SetActive(true);
        }
    }
}
