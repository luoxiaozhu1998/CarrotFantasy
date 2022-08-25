using Manager.NormalManager;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class NormalGameOptionSceneState : BaseSceneState
    {
        public NormalGameOptionSceneState(UIFacade uiFacade) : base(uiFacade)
        {
        }

        public override void EnterScene()
        {
            UIFacade.AddPanelToDict(Constants.PanelName.GameNormalOptionPanelName);
            UIFacade.AddPanelToDict(Constants.PanelName.GameNormalBigLevelPageName);
            UIFacade.AddPanelToDict(Constants.PanelName.GameNormalLevelPanelName);
            UIFacade.AddPanelToDict(Constants.PanelName.HelpPanelName);
            UIFacade.AddPanelToDict(Constants.PanelName.GameLoadPanelName);
            base.EnterScene();
        }

        public override void ExitScene()
        {
            base.ExitScene();
            SceneManager.LoadSceneAsync(
                UIFacade.CurrentSceneState.GetType() == typeof(NormalModelSceneState) ? 4 : 1);
        }
    }
}