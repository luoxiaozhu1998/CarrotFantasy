using Manager.NormalManager;
using TMPro;
using UI.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIPanel
{
    public class GameNormalBigLevelPanel : BasePanel
    {
        public Transform bigLevelContentTrans;
        public int bigLevelPageCount;
        private SlideScrollView m_SlideScrollView;
        private PlayerManager m_PlayerManager;
        private Transform[] m_BigLevelPages;

        private bool m_HasRegisterEvent;

        protected override void Awake()
        {
            base.Awake();
            m_PlayerManager = MuiFacade.PlayerManager;
            m_BigLevelPages = new Transform[bigLevelPageCount];
            m_SlideScrollView = transform.Find("Scroll View").GetComponent<SlideScrollView>();
            for (var i = 0; i < bigLevelPageCount; i++)
            {
                m_BigLevelPages[i] = bigLevelContentTrans.GetChild(i);
                ShowBigLevelState(m_PlayerManager.UnlockedNormalModelBigLevelLIst[i],
                    m_PlayerManager.UnlockedNormalModelLevelNum[i], 5, m_BigLevelPages[i], i + 1);
            }

            m_HasRegisterEvent = true;
        }

        private void OnEnable()
        {
            for (var i = 0; i < bigLevelPageCount; i++)
            {
                m_BigLevelPages[i] = bigLevelContentTrans.GetChild(i);
                ShowBigLevelState(m_PlayerManager.UnlockedNormalModelBigLevelLIst[i],
                    m_PlayerManager.UnlockedNormalModelLevelNum[i], 5, m_BigLevelPages[i], i + 1);
            }
        }

        public override void EnterPanel()
        {
            base.EnterPanel();
            m_SlideScrollView.Init();
            gameObject.SetActive(true);
        }


        public override void ExitPanel()
        {
            base.ExitPanel();
            gameObject.SetActive(false);
        }

        private void ShowBigLevelState(bool unlocked, int unlockedLevelNum, int totalNum,
            Transform bigLevelButtonTrans, int bigLevelID)
        {
            var bigLevelButtonComponent = bigLevelButtonTrans.GetComponent<Button>();
            if (unlocked) //解锁状态
            {
                bigLevelButtonTrans.Find("Img_Lock").gameObject.SetActive(false);
                bigLevelButtonTrans.Find("Img_Page").gameObject.SetActive(true);
                bigLevelButtonTrans.Find("Img_Page").Find("Txt_page").GetComponent<TMP_Text>()
                    .text = unlockedLevelNum.ToString() + '/' + totalNum;
                bigLevelButtonComponent.interactable = true;
                if (m_HasRegisterEvent) return;
                bigLevelButtonComponent.onClick.AddListener(() =>
                {
                    //离开大关卡页面
                    MuiFacade.CurrentScenePanelDict[
                            Constants.PanelName.GameNormalBigLevelPageName]
                        .ExitPanel();

                    var gameNormalLevelPanel =
                        MuiFacade.CurrentScenePanelDict[
                                Constants.PanelName.GameNormalLevelPanelName] as
                            GameNormalLevelPanel;
                    if (gameNormalLevelPanel != null)
                        gameNormalLevelPanel.ToThisPanel(bigLevelID);
                    var gameNormalOptionPanel = MuiFacade.CurrentScenePanelDict[
                        Constants.PanelName.GameNormalOptionPanelName] as GameNormalOptionPanel;
                    if (gameNormalOptionPanel != null)
                        gameNormalOptionPanel.isInBigLevelPanel = false;
                });
            }
            else
            {
                bigLevelButtonTrans.Find("Img_Lock").gameObject.SetActive(true);
                bigLevelButtonTrans.Find("Img_Page").gameObject.SetActive(false);
                bigLevelButtonComponent.interactable = false;
            }
        }

        public void ToNextPage()
        {
            m_SlideScrollView.ToNextPage();
        }

        public void ToLastPage()
        {
            m_SlideScrollView.ToLastPage();
        }
    }
}