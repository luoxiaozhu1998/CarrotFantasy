using Manager.MonoManager;
using UnityEngine;

namespace Manager.NormalManager
{
    /// <summary>
    /// 负责控制游戏音乐的播放和停止和游戏中所有的音效
    /// </summary>
    public class AudioSourceManager
    {
        private AudioSource[] _audioSources;

        private bool _playEffectMusic = true;
        private bool _playBgMusic = true;

        /// <summary>
        /// 构造函数
        /// </summary>
        public AudioSourceManager()
        {
            _audioSources = GameManager.Instance.GetComponents<AudioSource>();
        }

        public void PlayBgMusic(AudioClip audioClip)
        {
            if (_audioSources[0].isPlaying && _audioSources[0].clip == audioClip) return;
            _audioSources[0].clip = audioClip;
            _audioSources[0].Play();
        }

        public void PlayEffectMusic(AudioClip audioClip)
        {
            if (_playEffectMusic)
            {
                _audioSources[1].PlayOneShot(audioClip);
            }
        }

        public void CloseBgMusic()
        {
            _audioSources[0].Stop();
        }

        public void OpenBgMusic()
        {
            _audioSources[0].Play();
        }

        public void CloseOrOpenBgMusic()
        {
            _playBgMusic = !_playBgMusic;
            if (_playBgMusic)
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
            _playEffectMusic = !_playEffectMusic;
            
        }
        
    }
}