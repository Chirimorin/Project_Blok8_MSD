using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.Factories
{
    public abstract class ModelFactory
    {
        private static Database _database;

        public ModelFactory()
        {
            
        }

        public static Database Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new Database();
                }
                return _database;
            }
        }


    }
}
