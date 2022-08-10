using DG.Tweening;
using UnityEngine;

namespace UI.UIPanel
{
    public class HelpPanel : BasePanel
    {
        private GameObject m_HelpPageGo;
        private GameObject m_MonsterPageGo;
        private GameObject m_TowerPageGo;
        private SlideScrollView m_SlideScrollView;
        private SlideCanCoverScrollView m_SlideCanCoverScrollView;
        private Tween m_HelpPanelTween;

        protected override void Awake()
        {
            base.Awake();
            m_HelpPageGo = transform.Find("HelpPage").gameObject;
            m_MonsterPageGo = transform.Find("MonsterPage").gameObject;
            m_TowerPageGo = transform.Find("TowerPage").gameObject;
            m_SlideCanCoverScrollView = m_HelpPageGo.transform.Find("Scroll View")
                .GetComponent<SlideCanCoverScrollView>();
            m_SlideScrollView = m_TowerPageGo.transform.Find("Scroll View")
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
            m_SlideScrollView.Init();
        }
    }
}