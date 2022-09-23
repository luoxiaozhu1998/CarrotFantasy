using UnityEngine;

namespace Game.Builder
{
    public interface IBuilder<T>
    {
        /// <summary>
        /// 拿到怪物预制体身上的脚本
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        T GetProductClass(GameObject gameObject);

        /// <summary>
        /// 使用获取具体游戏对象
        /// </summary>
        /// <returns></returns>
        GameObject GetProduct();
        
        /// <summary>
        /// 获取数据信息
        /// </summary>
        /// <param name="productClassGo"></param>
        void GetData(T productClassGo);
        
        /// <summary>
        /// 获取特有资源信息
        /// </summary>
        /// <param name="productClassGo"></param>
        void GetOtherResource(T productClassGo);
    }
}