using DG.Tweening;
using UI.UI;
using UnityEngine;

namespace UI.UIPanel
{
    public class HelpPanel : BasePanel
    {
        private GameObject m_HelpPageGo;
        private GameObject m_MonsterPageGo;
        private GameObject m_TowerPageGo;
        private SlideScrollView m_HelpPageSlideScrollView;
        private SlideScrollView m_TowerPageSlideScrollView;
        private Tween m_HelpPanelTween;

        protected override void Awake()
        {
            base.Awake();
            m_HelpPageGo = transform.Find("HelpPage").gameObject;
            m_MonsterPageGo = transform.Find("MonsterPage").gameObject;
            m_TowerPageGo = transform.Find("TowerPage").gameObject;
            m_TowerPageSlideScrollView = m_HelpPageGo.transform.Find("Scroll View")
                .GetComponent<SlideScrollView>();
            m_HelpPageSlideScrollView = m_TowerPageGo.transform.Find("Scroll View")
                .GetComponent<SlideScrollView>();
            m_HelpPanelTween = transform.DOLocalMoveX(0, 0.5f);
            m_HelpPanelTween.SetAutoKill(false);
            m_HelpPanelTween.Pause();
        }

        public void ShowHelpPage()
        {
            m_HelpPageGo.SetActive(true);
            m_MonsterPageGo.SetActive(false);
            m_TowerPageGo.SetActive(false);
        }

        public void ShowMonsterPage()
        {
            m_HelpPageGo.SetActive(false);
            m_MonsterPageGo.SetActive(true);
            m_TowerPageGo.SetActive(false);
        }

        public void ShowTowerPage()
        {
            m_HelpPageGo.SetActive(false);
            m_MonsterPageGo.SetActive(false);
            m_TowerPageGo.SetActive(true);
        }

        public override void InitPanel()
        {
            base.InitPanel();
            transform.localPosition = new Vector3(1920, 0, 0);
            transform.SetSiblingIndex(5);
            //初始化两个ScrollView
            m_HelpPageSlideScrollView.Init();
            m_TowerPageSlideScrollView.Init();
            //初始显示help面板
            ShowHelpPage();
        }

        public override void EnterPanel()
        {
            base.EnterPanel();
            gameObject.SetActive(true);
            m_HelpPageSlideScrollView.Init();
            m_TowerPageSlideScrollView.Init();
            m_HelpPanelTween.PlayForward();
        }

        public override void ExitPanel()
        {
            base.ExitPanel();
            m_HelpPanelTween.PlayBackwards();
            //返回主面板
            MuiFacade.CurrentScenePanelDict[Constants.PanelName.MainPanelName].EnterPanel();
        }
    }
}