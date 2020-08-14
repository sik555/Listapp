using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listapp.Models
{
    public class ListappDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string UsersCollectionName { get; set; }
    }

    public interface IListappDatabaseSettings
    {
            string UsersCollectionName { get; set; }
            string ConnectionString { get; set; }
            string DatabaseName { get; set; }
        
    }
}