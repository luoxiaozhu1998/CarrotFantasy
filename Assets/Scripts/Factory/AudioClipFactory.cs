using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public class AudioClipFactory : IBaseResourceFactory<AudioClip>
    {
        /// <summary>
        /// string：路径，AudioClip：音频
        /// </summary>
        private readonly Dictionary<string, AudioClip> m_FactoryDict = new();

        private readonly string m_LoadPath;

        public AudioClipFactory()
        {
            m_LoadPath = "AudioClips/";
        }

        public AudioClip GetSingleResource(string resourcePath)
        {
            AudioClip itemGo;
            var itemPath = m_LoadPath + resourcePath;
            if (m_FactoryDict.ContainsKey(resourcePath))
            {
                itemGo = m_FactoryDict[resourcePath];
            }
            else
            {
                itemGo = Resources.Load<AudioClip>(itemPath);
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