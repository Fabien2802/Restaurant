using Restaurant.Shared.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Server.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(HttpContext httpContext);
        IGenericRepository<Customer> Customers { get; }
        IGenericRepository<Item> Items { get; }
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<Reservation> Reservations { get; }
        IGenericRepository<Sale> Sales { get; }
        IGenericRepository<Staff> Staffs { get; }
        IGenericRepository<Table> Tables { get; }
    }
}