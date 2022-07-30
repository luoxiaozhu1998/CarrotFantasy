using UnityEngine;

namespace Factory
{
    public interface IBaseFactory
    {
        GameObject GetItem(string itemName);
        
        void PushItem(string itemName, GameObject item);
    }
}