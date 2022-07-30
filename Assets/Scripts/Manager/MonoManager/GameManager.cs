using System;
using Manager.NormalManager;
using UnityEngine;

namespace Manager.MonoManager
{
    /// <summary>
    /// 游戏总管理，负责管理所有的管理者
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public PlayerManager PlayerManager;

        public FactoryManager FactoryManager;

        public AudioSourceManager AudioSourceManager;

        public UIManager UIManager;

        private static GameManager _instance;

        public static GameManager Instance => _instance;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
            PlayerManager = new PlayerManager();
            FactoryManager = new FactoryManager();
            AudioSourceManager = new AudioSourceManager();
            UIManager = new UIManager();
        }

        public GameObject CreateItem(GameObject itemGo)
        {
            var go = Instantiate(itemGo);
            return go;
        }
    }
}
