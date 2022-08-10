using System.Collections.Generic;
using Factory;

namespace Manager.NormalManager
{
    /// <summary>
    /// 工厂管理，负责管理所有的工厂和对象池
    /// </summary>
    public class FactoryManager
    {
        public readonly Dictionary<FactoryType, IBaseFactory> FactoryDict;

        public readonly AudioClipFactory AudioClipFactory;

        public readonly SpriteFactory SpriteFactory;

        public readonly RunTimeAnimatorControllerFactory RunTimeAnimatorControllerFactory;

        public FactoryManager()
        {
            FactoryDict = new Dictionary<FactoryType, IBaseFactory>
            {
                {FactoryType.UIPanelFactory, new UIPanelFactory()},
                {FactoryType.UIFactory, new UIFactory()},
                {FactoryType.GameFactory, new GameFactory()}
            };
            AudioClipFactory = new AudioClipFactory();
            SpriteFactory = new SpriteFactory();
            RunTimeAnimatorControllerFactory = new RunTimeAnimatorControllerFactory();
        }
    }
}
