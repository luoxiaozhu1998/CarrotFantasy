using Factory;
using Game;
using UI.UIPanel;
using UnityEngine;

namespace Manager.MonoManager
{
    /// <summary>
    /// 游戏控制管理，负责控制整个游戏逻辑
    /// </summary>
    public class GameController : MonoBehaviour
    {
        public static GameController instance { get; private set; }


        public Level Level;
        private GameManager m_GameManager;
        public int[] monsterIDList;
        public int monsterIDIndex;
        public Stage CurrentStage;
        public MapMaker mapMaker;

        public RuntimeAnimatorController[] Controllers;
        public NormalModelPanel normalModelPanel;

        private void Awake()
        {
#if Game
            instance = this;
            m_GameManager = GameManager.instance;
            CurrentStage = m_GameManager.CurrentStage;
            normalModelPanel =
                m_GameManager.UIManager.UIFacade.CurrentScenePanelDict[
                    Constants.PanelName.NormalModelPanelName] as NormalModelPanel;
            mapMaker = GetComponent<MapMaker>();
            mapMaker.InitMapMaker();
            mapMaker.LoadMap(CurrentStage.BigLevelID, CurrentStage.LevelID);
            Controllers = new RuntimeAnimatorController[12];
            for (var i = 0; i < Controllers.Length; i++)
            {
                Controllers[i] =
                    GetRuntimeAnimatorController("Monster/" + mapMaker.bigLevelID + "/" + (i + 1));
            }
#endif
        }

        public Sprite GetSprite(string resourcePath)
        {
            return m_GameManager.GetSprite(resourcePath);
        }

        public AudioClip GetAudioClip(string resourcePath)
        {
            return m_GameManager.GetAudioClip(resourcePath);
        }

        public RuntimeAnimatorController GetRuntimeAnimatorController(string resourcePath)
        {
            return m_GameManager.GetRuntimeAnimatorController(resourcePath);
        }

        public GameObject GetGameObject(string resourcePath)
        {
            return m_GameManager.GetGameObjectResource(factoryType: FactoryType.GameFactory,
                resPath: resourcePath);
        }

        public void PushGameObjectToFactory(string resourcePath, GameObject itemGo)
        {
            m_GameManager.PushGameObjectToFactory(FactoryType.GameFactory, resourcePath, itemGo);
        }
    }
}