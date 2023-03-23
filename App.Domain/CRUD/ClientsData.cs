using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.CRUD
{
    public class ClientsData
    {
        AppDbContext context;

        public ClientsData()
        {
            context = new AppDbContext();
        }

        public bool AddClient(Client client)
        {
            context.Clients.Add(client);
            context.SaveChanges();
            return true;
        }
    }
}
