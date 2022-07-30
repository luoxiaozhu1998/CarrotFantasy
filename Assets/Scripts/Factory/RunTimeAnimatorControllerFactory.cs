using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    /// <summary>
    /// 动画控制器资源工厂
    /// </summary>
    public class RunTimeAnimatorControllerFactory : IBaseResourceFactory<RuntimeAnimatorController>
    {
        /// <summary>
        /// string：路径，AudioClip：音频
        /// </summary>
        private readonly Dictionary<string, RuntimeAnimatorController> _factoryDict = new();

        private readonly string _loadPath;

        public RunTimeAnimatorControllerFactory()
        {
            _loadPath = "Animator/";
        }

        public RuntimeAnimatorController GetSingleResource(string resourcePath)
        {
            RuntimeAnimatorController itemGo;
            var itemPath = _loadPath + resourcePath;
            if (_factoryDict.ContainsKey(resourcePath))
            {
                itemGo = _factoryDict[resourcePath];
            }
            else
            {
                itemGo = Resources.Load<RuntimeAnimatorController>(itemPath);
                _factoryDict.Add(resourcePath, itemGo);
            }

            if (itemGo != null)
            {
                return itemGo;
            }

            Debug.Log(itemPath + "加载失败");
            return itemGo;
        }
    }
}