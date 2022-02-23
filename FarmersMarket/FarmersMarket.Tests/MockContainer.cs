namespace FarmersMarket.Tests
{
    using FarmersMarket.Data.Repositories;
    using FarmersMarket.Models.EntityModels;
    using FarmersMarket.Models.Enums;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MockContainer
    {
        public Mock<IRepository<Farm>>? FarmRepositoryMock { get; set; }
        public Mock<IRepository<Category>>? CategoryRepositoryMock { get; set; }
        public Mock<IRepository<Product>>? ProductRepositoryMock { get; set; }
        public Mock<IRepository<ShoppingCart>>? ShoppingCartRepositoryMock { get; set; }

        public void PrepareMocks()
        {
            this.SetupFakeFarms();
            this.SetupFakeCategories();
            this.SetupFakeProducts();
            this.SetupFakeShoppingCart();
        }

        private void SetupFakeFarms()
        {
            var fakeFarms = new List<Farm>()
            {
                new Farm()
                {
                    Id = 1,
                    Name = "Dido",
                    Address = "Svoboda 19",
                    Description = "Very cheap food",
                    Email = "dido@dido.com",
                    ImageUrl = "www.google.com",
                    PhoneNumber = "4165580102"
                },
                new Farm()
                {
                    Id = 2,
                    Name = "Mama",
                    Address = "Solunska 28B",
                    Description = "Very delicious food",
                    Email = "mama@mama.com",
                    ImageUrl = "www.google.com",
                    PhoneNumber = "+359899947852"
                },
                new Farm()
                {
                    Id = 3,
                    Name = "Rali",
                    Address = "Dianabad 43-44",
                    Description = "Very BIO food",
                    Email = "rali@rali.com",
                    ImageUrl = "www.google.com",
                    PhoneNumber = "+359888610285"
                }
            };

            this.FarmRepositoryMock = new Mock<IRepository<Farm>>();

            this.FarmRepositoryMock.Setup(r => r.All()).Returns(fakeFarms.AsQueryable());

            this.FarmRepositoryMock.Setup(r => r.Find(It.IsAny<int>()))
                .Returns((int id) =>
                {
                    var farm = fakeFarms.FirstOrDefault(f => f.Id == id);

                    return farm ?? null;
                });
        }

        private void SetupFakeCategories()
        {
            var fakeCategories = new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Fruits"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Vegetables"
                },
                new Category()
                {
                    Id = 3,
                    Name = "Dairy"
                }

            };

            this.CategoryRepositoryMock = new Mock<IRepository<Category>>();

            this.CategoryRepositoryMock.Setup(r => r.All()).Returns(fakeCategories.AsQueryable());

            this.CategoryRepositoryMock.Setup(r => r.Find(It.IsAny<int>()))
                .Returns((int id) =>
                {
                    var category = fakeCategories.FirstOrDefault(f => f.Id == id);

                    return category ?? null;
                });
            this.CategoryRepositoryMock.Setup(r => r.Add(It.IsAny<Category>())).Callback<Category>((category) => fakeCategories.Add(category));
            this.CategoryRepositoryMock.Setup(r => r.Remove(It.IsAny<Category>())).Callback<Category>((category) => fakeCategories.Remove(category));

        }

        private void SetupFakeProducts()
        {
            var fakeProducts = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "Tomatoes",
                    Description = "Very delicious",
                    Price = 1.69m,
                    Quantity = 1000,
                    Unit = "1kg",
                    ImageUrl = "http://flavoritetomatoes.com.au/wp-content/uploads/new_truss_tomatoes.jpg",
                    CategoryId = 1,
                    OwnerId = 1
                },
                new Product()
                {
                    Id = 1,
                    Name = "Oranges",
                    Description = "Very delicious fuits",
                    Price = 2.29m,
                    Quantity = 1500,
                    Unit = "1kg",
                    ImageUrl = "http://flavoritetomatoes.com.au/wp-content/uploads/new_truss_tomatoes.jpg",
                    CategoryId = 2,
                    OwnerId = 1
                },
                new Product()
                {
                    Id = 1,
                    Name = "Bananas",
                    Description = "Very delicious bananas",
                    Price = 1.99m,
                    Quantity = 500,
                    Unit = "1kg",
                    ImageUrl = "http://flavoritetomatoes.com.au/wp-content/uploads/new_truss_tomatoes.jpg",
                    CategoryId = 2,
                    OwnerId = 2
                }
            };

            this.ProductRepositoryMock = new Mock<IRepository<Product>>();

            this.ProductRepositoryMock.Setup(r => r.All()).Returns(fakeProducts.AsQueryable());

            this.ProductRepositoryMock.Setup(r => r.Find(It.IsAny<int>()))
                .Returns((int id) =>
                {
                    var product = fakeProducts.FirstOrDefault(f => f.Id == id);

                    return product ?? null;
                });
        }

        private void SetupFakeShoppingCart()
        {
            var fakeShoppingCarts = new List<ShoppingCart>()
            {
                new ShoppingCart()
                {
                    Id = 1,
                    UserId = "fd32mds343o324j34j319i43jmmfdasf",
                    Status = OrderStatus.Open,
                    TotalPrice = 12.29m,
                    DateOfDelivery = DateTime.Now,
                    DateOfOrder = DateTime.Now.AddDays(-1),
                    ShoppingCartProducts = new List<ShoppingCartProduct>()
                    {
                        new ShoppingCartProduct()
                        {
                            ShoppingCartId = 1,
                            ProductId = 2,
                            Units = 3
                        },
                        new ShoppingCartProduct()
                        {
                            ShoppingCartId = 2,
                            ProductId = 2,
                            Units = 2
                        },
                        new ShoppingCartProduct()
                        {
                            ShoppingCartId = 1,
                            ProductId = 1,
                            Units = 4
                        }
                    }
                },
                new ShoppingCart()
                {
                    Id = 2,
                    UserId = "fd32mds343ofdf4j34j319i43jmmfdasf",
                    Status = OrderStatus.Pending,
                    TotalPrice = 21.29m,
                    DateOfDelivery = DateTime.Now.AddDays(-1),
                    DateOfOrder = DateTime.Now.AddDays(-2),
                    ShoppingCartProducts = new List<ShoppingCartProduct>()
                    {
                        new ShoppingCartProduct()
                        {
                            ShoppingCartId = 1,
                            ProductId = 2,
                            Units = 1
                        },
                        new ShoppingCartProduct()
                        {
                            ShoppingCartId = 2,
                            ProductId = 1,
                            Units = 2
                        },
                        new ShoppingCartProduct()
                        {
                            ShoppingCartId = 2,
                            ProductId = 1,
                            Units = 3
                        }
                    }
                },
            };

            this.ShoppingCartRepositoryMock = new Mock<IRepository<ShoppingCart>>();

            this.ShoppingCartRepositoryMock.Setup(r => r.All()).Returns(fakeShoppingCarts.AsQueryable());

            this.ShoppingCartRepositoryMock.Setup(r => r.Find(It.IsAny<int>()))
                .Returns((int id) =>
                {
                    var shoppingCart = fakeShoppingCarts.FirstOrDefault(f => f.Id == id);

                    return shoppingCart ?? null;
                });
        }
    }
}
