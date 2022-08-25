using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public class SpriteFactory : IBaseResourceFactory<Sprite>
    {
        /// <summary>
        /// string：路径，AudioClip：音频
        /// </summary>
        private readonly Dictionary<string, Sprite> m_FactoryDict = new();

        private readonly string m_LoadPath;

        public SpriteFactory()
        {
            m_LoadPath = "Pictures/";
        }

        public Sprite GetSingleResource(string resourcePath)
        {
            Sprite itemGo;
            var itemPath = m_LoadPath + resourcePath;
            if (m_FactoryDict.ContainsKey(resourcePath))
            {
                itemGo = m_FactoryDict[resourcePath];
            }
            else
            {
                itemGo = Resources.Load<Sprite>(itemPath);
                m_FactoryDict.Add(resourcePath, itemGo);
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