using System;
using UnityEngine;


namespace Game
{
    public class Round : MonoBehaviour
    {
        
        [Serializable]
        public struct RoundInfo
        {
            public int[] monsterIDList;
            
        }

        public RoundInfo mRoundInfo;
    }
}