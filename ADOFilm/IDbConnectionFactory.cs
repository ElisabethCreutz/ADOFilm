using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ADOFilm
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection(); 
    }
}
