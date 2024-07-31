using entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxiServer
{
    public interface ICustomerProxy
    {
        Task<(Customer)> CreateAsync(Customer customer);
    }
}
