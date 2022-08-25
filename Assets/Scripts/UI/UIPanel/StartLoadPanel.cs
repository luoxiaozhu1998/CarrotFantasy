using Scenes;

namespace UI.UIPanel
{
    public class StartLoadPanel : BasePanel
    {
        protected override void Awake()
        {
            base.Awake();
            Invoke(nameof(LoadNextScene), 2f);
        }

        private void LoadNextScene()
        {
            MuiFacade.ChangeSceneState(new MainSceneState(MuiFacade));
        }
    }
}