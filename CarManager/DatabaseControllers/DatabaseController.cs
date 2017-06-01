using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace CarManager.DatabaseControllers
{
    
    public class DatabaseController
    {
        protected MySqlConnection conn;

        public DatabaseController()
        {
            conn = new MySqlConnection("Server=localhost;Database=carmanager;Uid=root;Pwd=root");
        }
    }
}
