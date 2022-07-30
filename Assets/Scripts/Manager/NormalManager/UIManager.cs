using System.Collections.Generic;
using Factory;
using Manager.MonoManager;
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
        private UIFacade _facade;
        
        //当前场景的PanelDict
        private Dictionary<string, GameObject> _currentScenePanelDict;
        
        private GameManager _manager;

        public UIManager()
        {
            _manager=GameManager.Instance;
            _currentScenePanelDict = new Dictionary<string, GameObject>();
            _facade = new UIFacade();
        }
        
    }
}
 