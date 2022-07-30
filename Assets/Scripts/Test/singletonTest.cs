using System;
using UnityEngine;

namespace Test
{
    public class SingletonTest<T> : MonoBehaviour
    where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance => _instance;

        private void Awake()
        {
            _instance = this as T;
        }
    }
}