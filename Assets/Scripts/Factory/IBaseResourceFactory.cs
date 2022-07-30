using UnityEngine;

namespace Factory
{
    public interface IBaseResourceFactory<T>
    {
        T GetSingleResource(string resourcePath);
    }
}