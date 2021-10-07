﻿using Basket.API.Entities;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string username);

        Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart);

        Task DeleteBasket(string username);
    }
}