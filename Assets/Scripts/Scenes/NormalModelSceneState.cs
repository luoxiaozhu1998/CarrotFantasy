using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class NormalModelSceneState : BaseSceneState
    {
        public NormalModelSceneState(UIFacade uiFacade) : base(uiFacade)
        {
        }

        public override void EnterScene()
        {
            UIFacade.AddPanelToDict(Constants.PanelName.GameLoadPanelName);
            UIFacade.AddPanelToDict(Constants.PanelName.NormalModelPanelName);
            base.EnterScene();
        }

        public override void ExitScene()
        {
            base.ExitScene();
            
        }
    }
}