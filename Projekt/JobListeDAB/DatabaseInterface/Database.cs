using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace DatabaseInterface
{
    public class Database
    {
        void AddUser(User user)
        {
            // Tilføjer et objekt i Bruger tabellen på databasen.
        }

        int ValidateLogInIndo(string email, string password)
        {
            /* Sammenligner email og password med databasen. Hvis der findes en bruger 
             * med matchende email og password returneres dennes bruger ID. Ellers returneres -1. */

        }

        User GetUserInfo(int userId)
        {
            // Returnerer et objekt af en bruger med matchende ID fra databasen.

        }

        void AddJob(Job job)
        {
            // Tilføjer et objekt i Job tabellen på databasen.

        }

        List<Job> GetJobList()
        {
            // Returnerer en liste over alle jobs i databasen.

        }
    }
}
