using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIPanel
{
    public class SetPanel : BasePanel
    {
        private GameObject m_OptionPageGo;
        private GameObject m_StatisticsPageGo;
        private GameObject m_ProducerPageGo;
        private GameObject m_PanelResetGo;


        private Tween m_SetPanelTween;
        private bool m_PlayBgMusic = true;
        private bool m_PlayEffectMusic = true;
        public Sprite[] btnSprites;
        private Image m_ImgBtnEffectAudio;
        private Image m_ImgBtnBgAudio;
        public Text[] statisticsTexts;

        protected override void Awake()
        {
            base.Awake();
            m_SetPanelTween = transform.DOLocalMoveX(0, 0.5f);
            m_SetPanelTween.SetAutoKill(false);
            m_SetPanelTween.Pause();
            m_OptionPageGo = transform.Find("OptionPage").gameObject;
            m_StatisticsPageGo = transform.Find("StatisticsPage").gameObject;
            m_ProducerPageGo = transform.Find("ProducerPage").gameObject;
            m_ImgBtnEffectAudio =
                m_OptionPageGo.transform.Find("Btn_EffectAudio").GetComponent<Image>();
            m_ImgBtnBgAudio =
                m_OptionPageGo.transform.Find("Btn_BGAudio").GetComponent<Image>();
            m_PanelResetGo = transform.Find("Panel_Reset").gameObject;
        }

        public override void InitPanel()
        {
            base.InitPanel();
            transform.localPosition = new Vector3(-1920, 0, 0);
            transform.SetSiblingIndex(2);
        }

        //显示页面的方法

        public void ShowOptionPage()
        {
            m_OptionPageGo.SetActive(true);
            m_StatisticsPageGo.SetActive(false);
            m_ProducerPageGo.SetActive(false);
        }

        public void ShowStatisticsPage()
        {
            m_OptionPageGo.SetActive(false);
            m_StatisticsPageGo.SetActive(true);
            m_ProducerPageGo.SetActive(false);
            ShowStatistics();
        }

        public void ShowProducerPage()
        {
            m_OptionPageGo.SetActive(false);
            m_StatisticsPageGo.SetActive(false);
            m_ProducerPageGo.SetActive(true);
        }

        public override void EnterPanel()
        {
            base.EnterPanel();
            ShowOptionPage();
            m_SetPanelTween.PlayForward();
        }

        public override void ExitPanel()
        {
            m_SetPanelTween.PlayBackwards();
            MuiFacade.CurrentScenePanelDict[Constants.PanelName.MainPanelName].EnterPanel();
            InitPanel();
        }

        /// <summary>
        /// 音乐处理
        /// </summary>
        public void CloseOrOpenBgMusic()
        {
            m_PlayBgMusic = !m_PlayBgMusic;
            MuiFacade.CloseOrOpenBgMusic();
            m_ImgBtnBgAudio.sprite = m_PlayBgMusic ? btnSprites[2] : btnSprites[3];
        }

        public void CloseOrOpenEffectMusic()
        {
            m_PlayEffectMusic = !m_PlayEffectMusic;
            MuiFacade.CloseOrEffectMusic();
            m_ImgBtnEffectAudio.sprite = m_PlayEffectMusic ? btnSprites[0] : btnSprites[1];
        }

        /// <summary>
        /// 显示数据
        /// </summary>
        private void ShowStatistics()
        {
            var playerManager = MuiFacade.MPlayerManager;
            statisticsTexts[0].text = playerManager.AdventureModelNum.ToString();
            statisticsTexts[1].text = playerManager.BuriedLevelNum.ToString();
            statisticsTexts[2].text = playerManager.BossModelNum.ToString();
            statisticsTexts[3].text = playerManager.Coin.ToString();
            statisticsTexts[4].text = playerManager.KillMonsterNum.ToString();
            statisticsTexts[5].text = playerManager.KillBossNum.ToString();
            statisticsTexts[6].text = playerManager.ClearItemNum.ToString();
        }

        /// <summary>
        /// 重置游戏
        /// </summary>
        public void ResetGame()
        {
        }

        public void ShowResetPanel()
        {
            m_PanelResetGo.SetActive(true);
        }

        public void CloseResetPanel()
        {
            m_PanelResetGo.SetActive(false);
        }
    }
}