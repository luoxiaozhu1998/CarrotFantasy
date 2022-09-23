using System.Collections.Generic;

namespace Game
{
    public class Level
    {
        public readonly int TotalRound;
        public readonly Round[] Rounds;
        public int CurrentRound;

        public Level(int roundNum, List<Round.RoundInfo> roundInfos)
        {
            TotalRound = roundNum;
            Rounds = new Round[TotalRound];
            for (var i = 0; i < TotalRound; i++)
            {
                Rounds[i] = new Round(roundInfos[i].monsterIDList, i, this);
            }

            for (var i = 0; i < TotalRound; i++)
            {
                if (i == TotalRound - 1)
                {
                    break;
                }

                Rounds[i].SetNextRound(Rounds[i + 1]);
            }
        }

        public void HandleRound()
        {
            if (CurrentRound >= TotalRound)
            {
                //胜利
            }
            else if (CurrentRound == TotalRound - 1)
            {
                //最后一波怪的UI显示，音乐播放
                
            }
            else
            {
                Rounds[CurrentRound].Handle(CurrentRound);
            }
        }
        /// <summary>
        /// 最后一回合的handle方法
        /// </summary>
        public void HandleLastRound()
        {
            Rounds[CurrentRound].Handle(CurrentRound);
        }

        public void AddRoundNum()
        {
            CurrentRound++;
        }
    }
}