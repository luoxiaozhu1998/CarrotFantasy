using UI;

namespace Scenes
{
    public class BaseSceneState : IBaseSceneState
    {
        protected readonly UIFacade UIFacade;

        public BaseSceneState(UIFacade uiFacade)
        {
            UIFacade = uiFacade;
        }

        public virtual void EnterScene()
        {
            UIFacade.InitDict();
        }

        public virtual void ExitScene()
        {
            UIFacade.ClearDict();
        }
    }
}