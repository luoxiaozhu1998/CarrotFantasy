using System.Collections.Generic;
using Factory;

namespace Manager.NormalManager
{
    /// <summary>
    /// 工厂管理，负责管理所有的工厂和对象池
    /// </summary>
    public class FactoryManager
    {
        public Dictionary<FactoryType, IBaseFactory> FactorieDict = new();

        public AudioClipFactory AudioClipFactory;

        public SpriteFactory SpriteFactory;

        public RunTimeAnimatorControllerFactory RunTimeAnimatorControllerFactory;

        public FactoryManager()
        {
            FactorieDict.Add(FactoryType.UIPanelFactory, new UIPanelFactory());
            FactorieDict.Add(FactoryType.UIFactory, new UIFactory());
            FactorieDict.Add(FactoryType.GameFactory, new GameFactory());
            AudioClipFactory = new AudioClipFactory();
            SpriteFactory = new SpriteFactory();
            RunTimeAnimatorControllerFactory = new RunTimeAnimatorControllerFactory();
        }
    }
}
