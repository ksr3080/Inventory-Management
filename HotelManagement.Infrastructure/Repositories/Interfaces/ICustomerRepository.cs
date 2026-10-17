using System;
using System.Collections.Generic;
using System.Text;
using HotelManagement.Domain.Entities;

namespace HotelManagement.Infrastructure.Repositories.Interfaces;

public interface ICustomerRepository
{
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken);

    Task<bool> GSTExistsAsync(string gstNo, CancellationToken cancellationToken);

    Task AddAsync(Customer customer, CancellationToken cancellationToken);

    Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken);

    Task<Customer?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    void Update(Customer customer);

    void Delete(Customer customer);
}
