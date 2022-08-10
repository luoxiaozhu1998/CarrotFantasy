using System;
using Manager.MonoManager;
using UnityEngine;

namespace UI.UIPanel
{
    public class BasePanel : MonoBehaviour, IBasePanel
    {
        protected UIFacade MuiFacade;
        
        public virtual void InitPanel()
        {
        }

        public virtual void EnterPanel()
        {
        }

        public virtual void ExitPanel()
        {
        }

        public virtual void UpdatePanel()
        {
        }

        protected virtual void Awake()
        {
            MuiFacade = GameManager.Instance.UIManager.UIFacade;
        }
    }
}