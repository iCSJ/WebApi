using BaseApi.Models;
using BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseApi.DAL
{
    public class ClientDAO: GenericDBContext<Client>
    {
        public Client GetByClientNo(string clientNo)
        {
            Client client = Items().Where(p => p.ClientNo == clientNo).FirstOrDefault();
            return client;
        }
    }
}