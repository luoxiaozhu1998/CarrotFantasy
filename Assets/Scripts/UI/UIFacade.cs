using System.Collections.Generic;
using DG.Tweening;
using Factory;
using Manager.MonoManager;
using Manager.NormalManager;
using Scenes;
using UI.UIPanel;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// UI中介，上层与管理者们做交互，下层与UI面板做交互
    /// </summary>
    public class UIFacade
    {
        #region Managers

        private UIManager _mUIManager;
        private readonly GameManager _mGameManager;
        private AudioSourceManager _mAudioSourceManager;
        public PlayerManager MPlayerManager;

        #endregion

        #region UIPanels

        public readonly Dictionary<string, IBasePanel> CurrentScenePanelDict = new();

        #endregion

        #region other Variables

        private GameObject mask;
        private Image _maskImage;
        public Transform canvasTransform;
        public IBaseSceneState CurrentSceneState;
        public IBaseSceneState LastSceneState;

        #endregion

        public UIFacade(UIManager uiManager)
        {
            _mGameManager = GameManager.Instance;
            MPlayerManager = _mGameManager.PlayerManager;
            _mUIManager = uiManager;
            _mAudioSourceManager = _mGameManager.AudioSourceManager;
            InitMask();
        }

        /// <summary>
        /// 初始化遮罩
        /// </summary>
        public void InitMask()
        {
            canvasTransform = GameObject.Find("Canvas").transform;
            mask = CreateUIAndSetPosition("ImageMask");
            _maskImage = mask.GetComponent<Image>();
        }

        /// <summary>
        /// 实例化当前场景的所有面板，并存入字典
        /// </summary>
        public void InitDict()
        {
            foreach (var item in _mUIManager.CurrentScenePanelDict)
            {
                
                item.Value.transform.SetParent(canvasTransform);
                item.Value.transform.localPosition = Vector3.zero;
                item.Value.transform.localScale = Vector3.one;
                var basePanel = item.Value.GetComponent<IBasePanel>();
                basePanel.InitPanel();
                CurrentScenePanelDict.Add(item.Key, basePanel);
            }
        }

        /// <summary>
        /// 改变场景状态
        /// </summary>
        /// <param name="baseSceneState"></param>
        public void ChangeSceneState(IBaseSceneState baseSceneState)
        {
            LastSceneState = CurrentSceneState;
            ShowMask();
            CurrentSceneState = baseSceneState;
        }

        public void ShowMask()
        {
            mask.transform.SetSiblingIndex(10);
            //透明度从0到1
            var t = DOTween.To(() => _maskImage.color, toColor => _maskImage.color = toColor,
                new Color(0, 0, 0, 1), 2f);

            //注册回调，播放完调用ExitSceneComplete();
            t.OnComplete(ExitSceneComplete);
        }

        private void ExitSceneComplete()
        {
            LastSceneState.ExitScene();
            CurrentSceneState.EnterScene();
            HideMask();
        }

        /// <summary>
        /// 隐藏遮罩
        /// </summary>
        public void HideMask()
        {
            mask.transform.SetSiblingIndex(10);
            DOTween.To(() => _maskImage.color, toColor => _maskImage.color = toColor,
                new Color(0, 0, 0, 0), 2f);
        }

        /// <summary>
        /// 清空字典
        /// </summary>
        public void ClearDict()
        {
            CurrentScenePanelDict.Clear();
            _mUIManager.ClearDict();
        }

        public void AddPanelToDict(string uiPanelName)
        {
            _mUIManager.CurrentScenePanelDict.Add(uiPanelName,
                GetGameObjectResource(FactoryType.UIPanelFactory, uiPanelName));
        }

        /// <summary>
        /// 创建需要的ui，设置ui父对象为Canvas，位置为原点，缩放为1
        /// </summary>
        /// <param name="uiName"></param>
        /// <returns>该ui对象</returns>
        public GameObject CreateUIAndSetPosition(string uiName)
        {
            var itemGo = GetGameObjectResource(FactoryType.UIFactory, uiName);
            itemGo.transform.SetParent(canvasTransform);
            itemGo.transform.localPosition = Vector3.zero;
            itemGo.transform.localScale = Vector3.one;
            return itemGo;
        }

        public Sprite GetSprite(string resPath)
        {
            return _mGameManager.GetSprite(resPath);
        }

        public AudioClip GetAudioClip(string resPath)
        {
            return _mGameManager.GetAudioClip(resPath);
        }

        public RuntimeAnimatorController GetRuntimeAnimatorController(string resPath)
        {
            return _mGameManager.GetRuntimeAnimatorController(resPath);
        }

        public GameObject GetGameObjectResource(FactoryType factoryType, string resPath)
        {
            return _mGameManager.GetGameObjectResource(factoryType, resPath);
        }

        public void PushGameObjectToFactory(FactoryType factoryType, string resPath,
            GameObject itemGo)
        {
            _mGameManager.PushGameObjectToFactory(factoryType, resPath, itemGo);
        }

        public void CloseOrEffectMusic()
        {
            _mAudioSourceManager.CloseOrEffectMusic();
        }

        public void CloseOrOpenBgMusic()
        {
            _mAudioSourceManager.CloseOrOpenBgMusic();
        }
        
        
    }
}