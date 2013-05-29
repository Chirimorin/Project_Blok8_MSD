using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSD.Models;

namespace MSD.Factories
{
    class db
    {
        private static Database database;

        public db()
        {
            database = new Database();
        }

        public static Database getInstance()
        {
            if (database == null)
            {
                database = new Database();
            }
            return database;
        }

        
    }
}
