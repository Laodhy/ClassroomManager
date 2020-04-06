using System;
using System.Collections.Generic;
using System.Text;

namespace ClassroomManager.Tools
{
    public abstract class Singleton<T>
         where T : Singleton<T>, new()
    {
        private static T _instance = new T();
        public static T Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
