using System.Collections.Generic;
using Factory;
using Manager.MonoManager;
using Manager.NormalManager;
using Scenes;
using TMPro;
using UI.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIPanel
{
    public class GameNormalLevelPanel : BasePanel
    {
        public int currentBigLevelID;

        private string m_FilePath;

        public int currentLevelID;

        private string m_SpritePath;

        private Transform m_LevelContentTrans;

        private GameObject m_ImgLockBtnGo;

        private Transform m_EmpTowerTrans;

        private Image m_ImgBgLeft;
        private Image m_ImgBgRight;

        private Image m_ImgCarrot;
        private Image m_ImgAllClear;
        private TMP_Text m_TotalWaves;
        private PlayerManager m_PlayerManager;
        private SlideScrollView m_SlideScrollView;
        private List<GameObject> m_LevelContentImageGos;
        private List<GameObject> m_TowerContentImageGos;


        protected override void Awake()
        {
            base.Awake();
            m_FilePath = "GameOption/Normal/Level/";
            m_PlayerManager = MuiFacade.PlayerManager;
            m_LevelContentImageGos = new List<GameObject>();
            m_TowerContentImageGos = new List<GameObject>();
            m_LevelContentTrans = transform.Find("Scroll View").Find("Viewport").Find("Content");
            m_ImgLockBtnGo = transform.Find("Img_Lock").gameObject;
            m_EmpTowerTrans = transform.Find("Emp_Tower");
            m_ImgBgLeft = transform.Find("Img_BGLeft").GetComponent<Image>();
            m_ImgBgRight = transform.Find("Img_BGRight").GetComponent<Image>();
            m_TotalWaves = transform.Find("Img_TotalWaves").Find("Text (TMP)")
                .GetComponent<TMP_Text>();
            m_SlideScrollView = transform.Find("Scroll View").GetComponent<SlideScrollView>();
            currentBigLevelID = 1;
            currentLevelID = 1;
            LoadResource();
        }

        private void LoadResource()
        {
            MuiFacade.GetSprite(m_FilePath + "AllClear");
            MuiFacade.GetSprite(m_FilePath + "Carrot_1");
            MuiFacade.GetSprite(m_FilePath + "Carrot_2");
            MuiFacade.GetSprite(m_FilePath + "Carrot_3");
            for (var i = 1; i < 4; i++)
            {
                var spritePath = m_FilePath + i + '/';
                MuiFacade.GetSprite(spritePath + "BG_Left");
                MuiFacade.GetSprite(spritePath + "BG_Right");
                for (var j = 1; j < 6; j++)
                {
                    MuiFacade.GetSprite(spritePath + "Level_" + j);
                }

                for (var j = 1; j < 13; j++)
                {
                    MuiFacade.GetSprite(m_FilePath + "Tower/Tower_" + j);
                }
            }
        }

        private void UpdateMapUI(string spritePath)
        {
            m_ImgBgLeft.sprite = MuiFacade.GetSprite(spritePath + "BG_Left");
            m_ImgBgRight.sprite = MuiFacade.GetSprite(spritePath + "BG_Right");
            for (var i = 0; i < 5; i++)
            {
                m_LevelContentImageGos.Add(CreateUIAndSetUIPosition("Img_Level",
                    m_LevelContentTrans));
                m_LevelContentImageGos[i].GetComponent<Image>().sprite =
                    MuiFacade.GetSprite(spritePath + "Level_" + (i + 1));
                var stage =
                    m_PlayerManager.UnlockedNormalModelLevelLIst[(currentBigLevelID - 1) * 5 + i];
                m_LevelContentImageGos[i].transform.Find("Img_Carrot").gameObject.SetActive(false);
                m_LevelContentImageGos[i].transform.Find("Img_AllClear").gameObject
                    .SetActive(false);
                if (stage.Unlocked) //是否解锁
                {
                    m_LevelContentImageGos[i].transform.Find("Img_Lock").gameObject
                        .SetActive(false);
                    m_LevelContentImageGos[i].transform.Find("Img_BG").gameObject
                        .SetActive(false);
                    if (stage.AllCLear)
                    {
                        m_LevelContentImageGos[i].transform.Find("Img_AllClear").gameObject
                            .SetActive(true);
                    }

                    if (stage.CarrotState == 0) continue;
                    var carrotImage = m_LevelContentImageGos[i].transform.Find("Img_Carrot")
                        .GetComponent<Image>();
                    carrotImage.gameObject.SetActive(true);
                    carrotImage.sprite =
                        MuiFacade.GetSprite(m_FilePath + "Carrot_" + stage.CarrotState);
                    
                }
                else
                {
                    if (stage.IsRewardLevel) //是奖励关卡
                    {
                        m_LevelContentImageGos[i].transform.Find("Img_Lock").gameObject
                            .SetActive(false);
                        m_LevelContentImageGos[i].transform.Find("Img_BG").gameObject
                            .SetActive(true);
                        var monsterPetImage = m_LevelContentImageGos[i].transform
                            .Find("Img_Monster")
                            .GetComponent<Image>();
                        monsterPetImage.sprite =
                            MuiFacade.GetSprite("MonsterNest/Monster/Baby/" + currentLevelID);
                        monsterPetImage.SetNativeSize();
                        monsterPetImage.transform.localScale = new Vector3(2, 2, 1);
                    }
                    else
                    {
                        m_LevelContentImageGos[i].transform.Find("Img_Lock").gameObject
                            .SetActive(true);
                        m_LevelContentImageGos[i].transform.Find("Img_BG").gameObject
                            .SetActive(false);
                    }
                }
            }

            //set the size of content
            m_SlideScrollView.SetContentLength(5);
        }

        private void DestroyMapUI()
        {
            if (m_LevelContentImageGos.Count <= 0) return;
            for (var i = 0; i < 5; i++)
            {
                MuiFacade.PushGameObjectToFactory(FactoryType.UIFactory, "Img_Level",
                    m_LevelContentImageGos[i]);
            }

            m_SlideScrollView.InitScrollLength();
            m_LevelContentImageGos.Clear();
        }

        private void UpdateLevelUI()
        {
            if (m_TowerContentImageGos.Count != 0)
            {
                foreach (var content in m_TowerContentImageGos)
                {
                    content.GetComponent<Image>().sprite = null;
                    MuiFacade.PushGameObjectToFactory(FactoryType.UIFactory, "Img_Tower", content);
                }

                m_TowerContentImageGos.Clear();
            }

            var stage = m_PlayerManager.UnlockedNormalModelLevelLIst[
                (currentBigLevelID - 1) * 5 + currentLevelID - 1];
            m_ImgLockBtnGo.SetActive(!stage.Unlocked);

            m_TotalWaves.text = stage.TotalRound.ToString();
            for (var i = 0; i < stage.TowerIDListLength; i++)
            {
                m_TowerContentImageGos.Add(CreateUIAndSetUIPosition("Img_Tower", m_EmpTowerTrans));
                m_TowerContentImageGos[i].GetComponent<Image>().sprite =
                    MuiFacade.GetSprite(m_FilePath + "Tower/" + "Tower_" +
                                        stage.TowerIDList[i]);
            }
        }

        public void ToThisPanel(int currentBigLevel)
        {
            currentBigLevelID = currentBigLevel;
            currentLevelID = 1;
            EnterPanel();
        }

        public override void ExitPanel()
        {
            base.ExitPanel();
            gameObject.SetActive(false);
        }

        public void ToGamePanel()
        {
            GameManager.instance.CurrentStage =
                m_PlayerManager.UnlockedNormalModelLevelLIst[
                    (currentBigLevelID - 1) * 5 + currentLevelID - 1];
            MuiFacade.CurrentScenePanelDict[Constants.PanelName.GameLoadPanelName].EnterPanel();
            MuiFacade.ChangeSceneState(new NormalModelSceneState(MuiFacade));
        }
        
        
        public override void InitPanel()
        {
            base.InitPanel();
            gameObject.SetActive(false);
        }

        public override void EnterPanel()
        {
            base.EnterPanel();
            gameObject.SetActive(true);
            m_SpritePath = m_FilePath + currentBigLevelID + '/';
            DestroyMapUI();
            UpdateMapUI(m_SpritePath);
            UpdateLevelUI();
            m_SlideScrollView.Init();
        }

        public override void UpdatePanel()
        {
            base.UpdatePanel();
            UpdateLevelUI();
        }

        private GameObject CreateUIAndSetUIPosition(string uiName, Transform parentTrans)
        {
            var itemGo =
                MuiFacade.GetGameObjectResource(factoryType: FactoryType.UIFactory, uiName);
            itemGo.transform.SetParent(parentTrans);
            itemGo.transform.localPosition = Vector3.zero;
            itemGo.transform.localScale = Vector3.one;
            return itemGo;
        }

        public void ToNextLevel()
        {
            currentLevelID++;
            UpdatePanel();
        }
        
        public void ToLastLevel()
        {
            currentLevelID--;
            UpdatePanel();
        }
    }
}