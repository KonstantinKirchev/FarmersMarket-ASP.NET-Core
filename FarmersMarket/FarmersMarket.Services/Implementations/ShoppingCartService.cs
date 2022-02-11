﻿namespace FarmersMarket.Services.Implementations
{
    using AutoMapper;
    using FarmersMarket.Data;
    using FarmersMarket.Models.EntityModels;
    using FarmersMarket.Models.Enums;
    using FarmersMarket.Models.ViewModels;
    using FarmersMarket.Services.Interfaces;

    public class ShoppingCartService : Service, IShoppingCartService
    {
        public ShoppingCartService(FarmersMarketDbContext db, IMapper mapper)
            : base(db, mapper)
        {
        }

        public Product? GetProduct(int id)
        {
            var product = this.db.Products.Find(id);
            return product;
        }

        public ShoppingCart GetShoppingCart(User user)
        {
            ShoppingCart? shoppingcart =
                this.db.ShoppingCarts
                    .FirstOrDefault(s => s.UserId == user.Id && s.Status == OrderStatus.Open);

            if (shoppingcart == null)
            {
                shoppingcart = new ShoppingCart()
                {
                    UserId = user.Id,
                    TotalPrice = 0.00m
                };

                this.db.ShoppingCarts.Add(shoppingcart);
                this.db.SaveChanges();
            }

            return shoppingcart;
        }

        public IEnumerable<ShoppingCartProductViewModel> MyShoppingCart(User user)
        {
            IEnumerable<ShoppingCartProduct> shoppingcarts =
                this.db.ShoppingCartProducts
                    .Where(s => s.ShoppingCart.UserId == user.Id && s.ShoppingCart.Status == OrderStatus.Open).ToList();

            IEnumerable<ShoppingCartProductViewModel> viewModels = this.mapper.Map<IEnumerable<ShoppingCartProduct>, IEnumerable<ShoppingCartProductViewModel>>(shoppingcarts);

            return viewModels;
        }

        public ShoppingCart? GetCurrentShoppingCart(int cartId)
        {
            ShoppingCart? shoppingcart = this.db.ShoppingCarts.Find(cartId);

            return shoppingcart;
        }

        public void AddToShoppingCart(ShoppingCart cart, Product product)
        {
            var products =
                this.db.ShoppingCartProducts
                .Where(sp => sp.ShoppingCartId == cart.Id).Select(sp => sp.Product).ToList();

            ShoppingCartProduct? shoppingCartProduct =
                this.db.ShoppingCartProducts
                .FirstOrDefault(sp => sp.ShoppingCartId == cart.Id && sp.ProductId == product.Id);

            if (products.Any())
            {
                foreach (var cartProduct in products)
                {
                    if (cartProduct.Id == product.Id)
                    {
                        if (shoppingCartProduct != null) shoppingCartProduct.Units++;
                        this.db.SaveChanges();
                        return;
                    }
                }
            }

            var newShoppingCartProduct = new ShoppingCartProduct()
            {
                ShoppingCartId = cart.Id,
                ProductId = product.Id
            };

            this.db.ShoppingCartProducts.Add(newShoppingCartProduct);
            this.db.SaveChanges();
        }

        public void RemoveFromShoppingCart(ShoppingCart cart, Product product)
        {
            var shoppingCartProduct =
                            this.db.ShoppingCartProducts
                            .SingleOrDefault(mc => mc.ShoppingCartId == cart.Id && mc.ProductId == product.Id);

            if (shoppingCartProduct != null)
            {
                this.db.ShoppingCartProducts.Remove(shoppingCartProduct);
                this.db.SaveChanges();
            }
        }

        public void DecreaseProductUnitsFromShoppingCart(ShoppingCart cart, Product product)
        {
            var shoppingCartProduct =
                            this.db.ShoppingCartProducts
                            .SingleOrDefault(mc => mc.ShoppingCartId == cart.Id && mc.ProductId == product.Id);

            if (shoppingCartProduct != null)
            {
                shoppingCartProduct.Units -= 1;

                if (shoppingCartProduct.Units == 0)
                {
                    this.db.ShoppingCartProducts.Remove(shoppingCartProduct);
                }
                this.db.SaveChanges();
            }
        }

        public void IncreaseProductUnitsFromShoppingCart(ShoppingCart cart, Product product)
        {
            var shoppingCartProduct =
                            this.db.ShoppingCartProducts
                            .SingleOrDefault(mc => mc.ShoppingCartId == cart.Id && mc.ProductId == product.Id);

            if (shoppingCartProduct != null)
            {
                shoppingCartProduct.Units += 1;
                this.db.SaveChanges();
            }
        }

        public void MakeAnOrder(int id, decimal totalAmount)
        {
            ShoppingCart? cart = this.db.ShoppingCarts.Find(id);

            if (cart != null)
            {
                cart.Status = OrderStatus.Pending;
                cart.TotalPrice = totalAmount;
                cart.DateOfOrder = DateTime.Now;

                this.db.SaveChanges();
            }
        }

        public bool IsProfileComplete(User user)
        {
            if (user.Name == null || user.Address == null || user.PhoneNumber == null)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<ShoppingCartProduct> GetOrderProducts(int id)
        {
            var products =
                this.db.ShoppingCartProducts
                .Where(sp => sp.ShoppingCartId == id).ToList();

            return products;
        }
    }
}