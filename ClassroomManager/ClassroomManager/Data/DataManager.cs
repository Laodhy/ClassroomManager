using ClassroomManager.Models;
using ClassroomManager.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassroomManager.Data
{
    public class DataManager : Singleton<DataManager>
    {
        public User CurrentUser { get; set; }

        public DataManager()
        {
            CurrentUser = null;
        }

    }
}
