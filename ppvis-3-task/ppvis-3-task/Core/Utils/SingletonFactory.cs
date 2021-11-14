using System;

namespace Core.Utils
{
    public abstract class SingletonFactory<T> where T : class
    {
        public static T Instance { get; private set; }

        public SingletonFactory()
        {
            if (Instance != null)
            {
                throw new ArgumentException("There is one than more singleton instances");
            }

            Instance = this as T;
        }
    }
}
