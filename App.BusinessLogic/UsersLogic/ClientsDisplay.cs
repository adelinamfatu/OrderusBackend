using App.BusinessLogic.Helper;
using App.Domain.CRUD;
using App.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BusinessLogic.UsersLogic
{
    public class ClientsDisplay
    {
        ClientsData clientsData;

        public ClientsDisplay()
        {
            clientsData = new ClientsData();
        }

        public bool AddClient(ClientDTO client)
        {
            return clientsData.AddClient(DTOEntity.DTOtoEntity(client));
        }

        public string Login(ClientDTO client)
        {
            return clientsData.Login(DTOEntity.DTOtoEntity(client));
        }

        public ClientDTO GetClient(string username)
        {
            return EntityDTO.EntityToDTO(clientsData.GetClient(username));
        }

        public IEnumerable<OrderDTO> GetOrders(string email)
        {
            return clientsData.GetOrders(email).Select(order => EntityDTO.EntityToDTO(order));
        }

        public void UpdatePicture(string fileName)
        {
            clientsData.UpdatePicture(fileName);
        }

        public bool UpdateClientDetails(ClientDTO client)
        {
            return clientsData.UpdateClientDetails(DTOEntity.DTOtoEntity(client));
        }

        public bool AddOffer(OfferDTO offer)
        {
            return clientsData.AddOffer(DTOEntity.DTOtoEntity(offer));
        }

        public IEnumerable<OfferDTO> GetOffers(string email)
        {
            return clientsData.GetOffers(email).Select(o => EntityDTO.EntityToDTO(o));
        }
    }
}
