using Manager.MonoManager;
using UnityEngine;

namespace Game.Builder
{
    public class MonsterBuilder : IBuilder<Monster>
    {
        private int m_MonsterID;
        private GameObject m_MonsterGo;
        public Monster GetProductClass(GameObject gameObject)
        {
            return gameObject.GetComponent<Monster>();
        }

        public GameObject GetProduct()
        {
            var itemGo = GameController.instance.GetGameObject("MonsterPrefab");
            var monster = GetProductClass(itemGo);
            GetData(monster);
            GetOtherResource(monster);
            if (monster == null)
            {
                Debug.Log("GetProduct() Fail!");
            }
            return itemGo;
        }

        public void GetData(Monster productClassGo)
        {
            productClassGo.monsterID = m_MonsterID;
            productClassGo.hP = m_MonsterID * 100;
            productClassGo.currentHp = productClassGo.hP;
            productClassGo.moveSpeed = m_MonsterID;
            productClassGo.prize = m_MonsterID * 50;
        }

        public void GetOtherResource(Monster productClassGo)
        {
            productClassGo.GetMonsterProperty();
        }
    }
}