using Manager.MonoManager;
using UnityEngine;

namespace Manager.NormalManager
{
    /// <summary>
    /// 负责控制游戏音乐的播放和停止和游戏中所有的音效
    /// </summary>
    public class AudioSourceManager
    {
        private readonly AudioSource[] m_AudioSources;

        private bool m_PlayEffectMusic = true;
        private bool m_PlayBgMusic = true;

        /// <summary>
        /// 构造函数
        /// </summary>
        public AudioSourceManager()
        {
            m_AudioSources = GameManager.instance.GetComponents<AudioSource>();
        }

        public void PlayBgMusic(AudioClip audioClip)
        {
            if (m_AudioSources[0].isPlaying && m_AudioSources[0].clip == audioClip) return;
            m_AudioSources[0].clip = audioClip;
            m_AudioSources[0].Play();
        }

        public void PlayEffectMusic(AudioClip audioClip)
        {
            if (m_PlayEffectMusic)
            {
                m_AudioSources[1].PlayOneShot(audioClip);
            }
        }

        private void CloseBgMusic()
        {
            m_AudioSources[0].Stop();
        }

        private void OpenBgMusic()
        {
            m_AudioSources[0].Play();
        }

        public void CloseOrOpenBgMusic()
        {
            m_PlayBgMusic = !m_PlayBgMusic;
            if (m_PlayBgMusic)
            {
                OpenBgMusic();
            }
            else
            {
                CloseBgMusic();
            }
        }
        public void CloseOrEffectMusic()
        {
            m_PlayEffectMusic = !m_PlayEffectMusic;
            
        }
        
    }
}