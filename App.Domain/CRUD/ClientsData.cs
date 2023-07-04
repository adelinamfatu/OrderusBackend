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

        public IEnumerable<Order> GetOrders(string email)
        {
            return context.Orders.Where(o => o.ClientEmail == email).OrderByDescending(o => o.DateTime);
        }

        public bool UpdateClientDetails(Client client)
        {
            var existingClient = context.Clients.Where(e => e.Email == client.Email).FirstOrDefault();
            if (existingClient.City != client.City)
            {
                existingClient.City = client.City;
            }
            if (existingClient.Street != client.Street)
            {
                existingClient.Street = client.Street;
            }
            if (existingClient.StreetNumber != client.StreetNumber)
            {
                existingClient.StreetNumber = client.StreetNumber;
            }
            if (existingClient.Building != client.Building)
            {
                existingClient.Building = client.Building;
            }
            if (existingClient.Staircase != client.Staircase)
            {
                existingClient.Staircase = client.Staircase;
            }
            if (existingClient.ApartmentNumber != client.ApartmentNumber)
            {
                existingClient.ApartmentNumber = client.ApartmentNumber;
            }
            if (existingClient.Floor != client.Floor)
            {
                existingClient.Floor = client.Floor;
            }
            context.SaveChanges();
            return true;
        }

        public void UpdatePicture(string fileName)
        {
            var client = context.Clients.FirstOrDefault(c => c.Email == fileName.Substring(0, fileName.Length - 4));
            client.Picture = Resource.IISAddress + fileName;
            context.SaveChanges();
        }

        public bool AddOffer(Offer offer)
        {
            context.Offers.Add(offer);
            context.SaveChanges();
            return true;
        }

        public IEnumerable<Offer> GetOffers(string email)
        {
            DateTime currentDate = DateTime.Now;
            DateTime startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

            return context.Offers
                .Where(o => o.ClientEmail == email && o.ExpirationDate >= startOfMonth);
        }

        public IEnumerable<Order> GetClientUncommentedOrders(string email)
        {
            return context.Orders.Where(o => o.ClientEmail == email && !context.Comments.Any(c => c.OrderID == o.ID));
        }

        public IEnumerable<Offer> GetAvailableOffers(string email)
        {
            DateTime currentDate = DateTime.Now;
            return context.Offers.Where(o => o.ClientEmail == email && 
                                                    ((o.ExpirationDate.Year < currentDate.Year) ||
                                                    (o.ExpirationDate.Month <= currentDate.Month) ||
                                                    (o.ExpirationDate.Month == currentDate.Month && o.ExpirationDate.Day <= currentDate.Day)));
        }
    }
}
