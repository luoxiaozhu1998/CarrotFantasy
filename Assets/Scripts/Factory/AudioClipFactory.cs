using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public class AudioClipFactory : IBaseResourceFactory<AudioClip>
    {
        /// <summary>
        /// string：路径，AudioClip：音频
        /// </summary>
        protected readonly Dictionary<string, AudioClip> FactoryDict = new();

        private readonly string _loadPath;

        public AudioClipFactory()
        {
            _loadPath = "AudioClips/";
        }

        public AudioClip GetSingleResource(string resourcePath)
        {
            AudioClip itemGo;
            var itemPath = _loadPath + resourcePath;
            if (FactoryDict.ContainsKey(resourcePath))
            {
                itemGo = FactoryDict[resourcePath];
            }
            else
            {
                itemGo = Resources.Load<AudioClip>(itemPath);
                FactoryDict.Add(resourcePath, itemGo);
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