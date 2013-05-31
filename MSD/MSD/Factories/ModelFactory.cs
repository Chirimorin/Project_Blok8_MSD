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

        private static Database database;

        public ModelFactory()
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
