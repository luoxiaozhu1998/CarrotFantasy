using System;
using Factory;
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

        public static GameManager instance { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            PlayerManager = new PlayerManager();
            FactoryManager = new FactoryManager();
            AudioSourceManager = new AudioSourceManager();
            UIManager = new UIManager();
            UIManager.UIFacade.CurrentSceneState.EnterScene();
        }

        public GameObject CreateItem(GameObject itemGo)
        {
            var go = Instantiate(itemGo);
            return go;
        }

        /// <summary>
        /// 获取Sprite
        /// </summary>
        /// <param name="resPath"></param>
        /// <returns></returns>
        public Sprite GetSprite(string resPath)
        {
            return FactoryManager.SpriteFactory.GetSingleResource(resPath);
        }

        /// <summary>
        /// 获取AudioClip
        /// </summary>
        /// <param name="resPath"></param>
        /// <returns></returns>
        public AudioClip GetAudioClip(string resPath)
        {
            return FactoryManager.AudioClipFactory.GetSingleResource(resPath);
        }
        /// <summary>
        /// 获取RuntimeAnimatorController
        /// </summary>
        /// <param name="resPath"></param>
        /// <returns></returns>
        public RuntimeAnimatorController GetRuntimeAnimatorController(string resPath)
        {
            return FactoryManager.RunTimeAnimatorControllerFactory.GetSingleResource(resPath);
        }

        public GameObject GetGameObjectResource(FactoryType factoryType,string resPath)
        {
            return FactoryManager.FactoryDict[factoryType].GetItem(resPath);
        }

        public void PushGameObjectToFactory(FactoryType factoryType, string resPath,
            GameObject itemGo)
        {
            FactoryManager.FactoryDict[factoryType].PushItem(resPath, itemGo);
        }
    }
}