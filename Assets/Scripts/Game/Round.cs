using System;


namespace Game
{
    public class Round
    {
        
        [Serializable]
        public struct RoundInfo
        {
            public int[] monsterIDList;
            
        }

        public RoundInfo MRoundInfo;
        protected Round NextRound;
        protected int RoundID;
        protected Level Level;

        public Round(int[] monsterIDList,int roundID,Level level)
        {
            Level = level;
            RoundID = roundID;
            MRoundInfo.monsterIDList = monsterIDList;
        }

        public void SetNextRound(Round nextRound)
        {
            NextRound = nextRound;
        }

        public void Handle(int roundID)
        {
            if (RoundID < roundID)
            {
                NextRound.Handle(roundID);
            }
            else
            {
                //产生怪物
            }
        }
    }
}