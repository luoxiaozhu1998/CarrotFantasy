using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class LevelInfo
    {
        public int BigLevelID;
        public int LevelID;

        public GridPoint.GridState[,] GridStates;

        public List<GridPoint.GridIndex> MonsterPaths;

        public List<Round.RoundInfo> RoundInfos;
        
    }
}