using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public class SpriteFactory : IBaseResourceFactory<Sprite>
    {
        /// <summary>
        /// string：路径，AudioClip：音频
        /// </summary>
        private readonly Dictionary<string, Sprite> _factoryDict = new();

        private readonly string _loadPath;

        public SpriteFactory()
        {
            _loadPath = "Pictures/";
        }

        public Sprite GetSingleResource(string resourcePath)
        {
            Sprite itemGo;
            var itemPath = _loadPath + resourcePath;
            if (_factoryDict.ContainsKey(resourcePath))
            {
                itemGo = _factoryDict[resourcePath];
            }
            else
            {
                itemGo = Resources.Load<Sprite>(itemPath);
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