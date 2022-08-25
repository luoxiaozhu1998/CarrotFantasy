using UI;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class MainSceneState : BaseSceneState
    {
        public MainSceneState(UIFacade uiFacade) : base(uiFacade)
        {
            
        }

        public override void EnterScene()
        {
            UIFacade.AddPanelToDict(Constants.PanelName.MainPanelName);
            UIFacade.AddPanelToDict(Constants.PanelName.SetPanelName);
            UIFacade.AddPanelToDict(Constants.PanelName.HelpPanelName);
            UIFacade.AddPanelToDict(Constants.PanelName.GameLoadPanelName);
            base.EnterScene();
        }

        public override void ExitScene()
        {
            base.ExitScene();
            //当前对象的类类型,CurrentSceneState已经是下一个状态了
            if (UIFacade.CurrentSceneState.GetType() == typeof(NormalGameOptionSceneState))
            {
                SceneManager.LoadScene(2);
            }
            else if (UIFacade.CurrentSceneState.GetType() == typeof(BossGameOptionSceneState))
            {
                SceneManager.LoadScene(3);
            }
            else
            {
                SceneManager.LoadScene(6);
            }
        }
    }
}