using Manager.MonoManager;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class Monster : MonoBehaviour
    {
        public int monsterID;
        /// <summary>
        /// 总血量
        /// </summary>
        public int hP;
        /// <summary>
        /// 当前血量
        /// </summary>
        public int currentHp;
        /// <summary>
        /// 当前速度
        /// </summary>
        public float moveSpeed;
        /// <summary>
        /// 初始速度
        /// </summary>
        public float initMoveSpeed;
        /// <summary>
        /// 奖励金钱
        /// </summary>
        public int prize;

        private Animator m_Animator;
        private RuntimeAnimatorController m_RuntimeAnimatorController;
        public void GetMonsterProperty()
        {
            m_Animator.runtimeAnimatorController = GameController.instance.Controllers[monsterID - 1];
            
        }
    }
}