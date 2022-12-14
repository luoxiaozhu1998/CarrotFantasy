using System.Collections.Generic;
using Factory;
using Manager.MonoManager;
using Scenes;
using UI;
using UnityEngine;

namespace Manager.NormalManager
{
    /// <summary>
    /// 负责管理UI
    /// </summary>
    public class UIManager
    {
        /// <summary>
        /// 中介者模式
        /// </summary>
        public readonly UIFacade UIFacade;

        //当前场景的PanelDict
        public readonly Dictionary<string, GameObject> CurrentScenePanelDict;

        private readonly GameManager m_Manager;

        public UIManager()
        {
            m_Manager = GameManager.instance;
            CurrentScenePanelDict = new Dictionary<string, GameObject>();
            UIFacade = new UIFacade(this);
            UIFacade.CurrentSceneState = new StartLoadSceneState(UIFacade);
        }

        private void PushUIPanel(string uiPanelName, GameObject uiPanelGo)
        {
            m_Manager.PushGameObjectToFactory(FactoryType.UIPanelFactory, uiPanelName, uiPanelGo);
        }

        /// <summary>
        /// 清空字典
        /// </summary>
        public void ClearDict()
        {
            foreach (var item in CurrentScenePanelDict)
            {

                PushUIPanel(item.Value.name[..^7], item.Value);
            }

            CurrentScenePanelDict.Clear();
        }
    }
}