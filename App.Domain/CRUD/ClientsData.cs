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
            var existingClient = context.Clients.FirstOrDefault(c => c.Email == client.Email);
            if(existingClient != null)
            {
                return false;
            }
            else
            {
                context.Clients.Add(client);
                context.SaveChanges();
                return true;
            }
        }

        public string Login(Client client)
        {
            return context.Clients.FirstOrDefault(c => c.Email == client.Email).Password;
        }

        public Client GetClient(string email)
        {
            return context.Clients.FirstOrDefault(c => c.Email == email);
        }
    }
}
