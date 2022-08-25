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

        public PlayerManager()
        {
            AdventureModelNum = 100;
            BuriedLevelNum = 100;
            BossModelNum = 100;
            Coin = 100;
            KillMonsterNum = 100;
            KillBossNum = 100;
            ClearItemNum = 100;
            UnlockedNormalModelBigLevelLIst = new List<bool>
            {
                true,
                true,
                true
            };
            UnlockedNormalModelLevelLIst = new List<Stage>
            {
                new(10,2,false,true,1,1,0,false,new[]{1,2}),
                new(10,2,false,true,1,2,0,false,new[]{2,2}),
                new(10,2,false,true,1,3,0,false,new[]{3,2}),
                new(10,2,false,true,1,4,0,false,new[]{4,2}),
                new(10,2,false,true,1,5,0,false,new[]{5,2}),
                new(10,2,false,true,2,1,0,false,new[]{1,2}),
                new(10,2,false,true,2,2,0,false,new[]{2,2}),
                new(10,2,false,true,2,3,0,false,new[]{3,2}),
                new(10,2,false,true,2,4,0,false,new[]{4,2}),
                new(10,2,false,true,2,5,0,false,new[]{5,2}),
            };
            UnlockedNormalModelLevelNum = new List<int>()
            {
                2, 2, 2
            };
        }
    }
}