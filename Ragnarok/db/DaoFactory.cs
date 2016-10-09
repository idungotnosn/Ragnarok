using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ragnarok.db
{
    class DaoFactory
    {
        public static IDao createDao()
        {
            return new MySqlDao();
        }
    }
}
