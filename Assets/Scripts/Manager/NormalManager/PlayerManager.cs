using System.Collections.Generic;
using Game;

namespace Manager.NormalManager
{
    /// <summary>
    /// 玩家的管理，负责保存和加载玩家数据
    /// </summary>
    public class PlayerManager
    {
        public int AdventureModelNum;
        public int BuriedLevelNum;
        public int BossModelNum;
        public int Coin;
        public int KillMonsterNum;
        public int KillBossNum;
        public int ClearItemNum;
        public List<bool> UnlockedNormalModelBigLevelLIst;
        public List<Stage> UnlockedNormalModelLevelLIst;
        public List<int> UnlockedNormalModelLevelNum;

        public int Cookies;
        public int Milk;
        public int Nest;
        public int Diamonds;
    }
}
