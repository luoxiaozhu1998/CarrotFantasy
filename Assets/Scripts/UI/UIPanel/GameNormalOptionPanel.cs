using Scenes;
using UnityEngine;

namespace UI.UIPanel
{
    public class GameNormalOptionPanel : BasePanel
    {
        [HideInInspector]
        public bool isInBigLevelPanel = true;

        public void ReturnToLastPanel()
        {
            if (isInBigLevelPanel)
            {
                //返回主界面
                MuiFacade.ChangeSceneState(new MainSceneState(MuiFacade));
            }
            else
            {
                MuiFacade.CurrentScenePanelDict[Constants.PanelName.GameNormalLevelPanelName]
                    .ExitPanel();
                MuiFacade.CurrentScenePanelDict[Constants.PanelName.GameNormalBigLevelPageName]
                    .EnterPanel();
            }

            isInBigLevelPanel = true;
        }

        public void ToHelpPanel()
        {
            MuiFacade.CurrentScenePanelDict[Constants.PanelName.HelpPanelName].EnterPanel();
        }
    }
}