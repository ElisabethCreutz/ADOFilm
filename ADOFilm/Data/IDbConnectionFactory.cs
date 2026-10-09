using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ADOFilm.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection(); 
    }
}
