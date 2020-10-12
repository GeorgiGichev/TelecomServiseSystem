﻿namespace TelecomServiceSystem.Services.Data.Addresses
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAddressService
    {
        Task CreateAsync<T>(T model);

        Task<IEnumerable<T>> GetByCustomerIdAsync<T>(string customerId);

        Task<T> GetByIdAsync<T>(int id);

        Task EditAsync<T>(T input);
    }
}