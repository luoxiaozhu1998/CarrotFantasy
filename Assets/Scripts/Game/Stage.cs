namespace Game
{
    public class Stage
    {
        public int[] TowerIDList;
        public int TowerIDListLength;
        public bool AllCLear;
        public int CarrotState;
        public int LevelID;
        public int BigLevelID;
        public bool Unlocked;
        public bool IsRewardLevel;
        public int TotalRound;

        public Stage(int totalRound, int towerIDListLength, bool isRewardLevel, bool unlocked,
            int bigLevelID, int levelID, int carrotState, bool allCLear, int[] towerIDList)
        {
            TotalRound = totalRound;
            TowerIDListLength = towerIDListLength;
            IsRewardLevel = isRewardLevel;
            Unlocked = unlocked;
            BigLevelID = bigLevelID;
            LevelID = levelID;
            CarrotState = carrotState;
            AllCLear = allCLear;
            TowerIDList = towerIDList;
        }
    }
}