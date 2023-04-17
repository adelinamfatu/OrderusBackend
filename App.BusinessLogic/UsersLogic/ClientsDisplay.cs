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
    }
}
