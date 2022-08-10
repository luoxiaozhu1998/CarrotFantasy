using DG.Tweening;
using Scenes;
using UnityEngine;

namespace UI.UIPanel
{
    public class MainPanel : BasePanel
    {
        private Animator m_CarrotAnimator;
        private Transform m_MonsterTrans;
        private Transform m_CloudTrans;
        private Tween[] m_MainPanelTween;
        private Tween m_ExitTween;

        protected override void Awake()
        {
            base.Awake();
            transform.SetSiblingIndex(8);
            m_CarrotAnimator = transform.Find("Emp_Carrot").GetComponent<Animator>();
            m_CarrotAnimator.Play("CarrotGrow");
            m_MonsterTrans = transform.Find("Img_Monster");
            m_CloudTrans = transform.Find("Img_Cloud");
            m_MainPanelTween = new Tween[2];
            m_MainPanelTween[0] = transform.DOLocalMoveX(1920, 0.5f);
            m_MainPanelTween[0].SetAutoKill(false);
            m_MainPanelTween[0].Pause();
            m_MainPanelTween[1] = transform.DOLocalMoveX(-1920, 0.5f);
            m_MainPanelTween[1].SetAutoKill(false);
            m_MainPanelTween[1].Pause();
            PlayUITween();
        }

        public override void EnterPanel()
        {
            m_CarrotAnimator.Play("CarrotGrow");
            m_ExitTween?.PlayBackwards();
            m_CloudTrans.gameObject.SetActive(true);
        }

        public override void ExitPanel()
        {
            base.ExitPanel();
            m_ExitTween.PlayForward();
            m_CloudTrans.gameObject.SetActive(false);
        }

        /// <summary>
        /// UI动画播放
        /// </summary>
        private void PlayUITween()
        {
            m_MonsterTrans.DOLocalMoveY(550, 2f).SetLoops(-1, LoopType.Yoyo);
            m_CloudTrans.DOLocalMoveX(1300, 8f).SetLoops(-1, LoopType.Restart);
        }

        public void MoveToRight()
        {
            m_ExitTween = m_MainPanelTween[0];
            MuiFacade.CurrentScenePanelDict[Constants.PanelName.SetPanelName].EnterPanel();
        }

        public void MoveToLeft()
        {
            m_ExitTween = m_MainPanelTween[1];
            MuiFacade.CurrentScenePanelDict[Constants.PanelName.HelpPanelName].EnterPanel();
        }

        //场景切换
        public void ToNormalModelScene()
        {
            MuiFacade.CurrentScenePanelDict[Constants.PanelName.GameLoadPanelName].EnterPanel();
            MuiFacade.ChangeSceneState(new NormalGameOptionSceneState(MuiFacade));
        }

        public void ToBossModelScene()
        {
            MuiFacade.CurrentScenePanelDict[Constants.PanelName.GameLoadPanelName].EnterPanel();
            MuiFacade.ChangeSceneState(new BossGameOptionSceneState(MuiFacade));
        }

        public void ToMonsterNestScene()
        {
            MuiFacade.CurrentScenePanelDict[Constants.PanelName.GameLoadPanelName].EnterPanel();
            MuiFacade.ChangeSceneState(new MonsterNestSceneState(MuiFacade));
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}