using Constants;
using UI;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class StartLoadSceneState : BaseSceneState
    {
        public StartLoadSceneState(UIFacade uiFacade) : base(uiFacade)
        {
        }

        public override void EnterScene()
        {
            UIFacade.AddPanelToDict(PanelName.StartLoadPanelName);
            base.EnterScene();
        }

        public override void ExitScene()
        {
            base.ExitScene();
            SceneManager.LoadScene(1);
        }
    }
}