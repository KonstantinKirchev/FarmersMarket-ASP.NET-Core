namespace FarmersMarket.Services.Implementations
{
    using AutoMapper;
    using FarmersMarket.Data.UnitOfWork;
    using FarmersMarket.Models.BindingModels;
    using FarmersMarket.Models.EntityModels;
    using FarmersMarket.Models.ViewModels;
    using FarmersMarket.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ProductsService : Service, IProductsService
    {
        public ProductsService(IFarmersMarketData db, IMapper mapper) 
            : base(db, mapper)
        {
        }

        public IEnumerable<ProductViewModel> GetAllProducts()
        {
            IEnumerable<Product> products = this.db.Products.All().Where(p => p.IsDeleted == false && p.Category.IsDeleted == false && p.Owner.IsDeleted == false).OrderBy(p => p.Id).ToList();
            IEnumerable<ProductViewModel> viewModels = this.mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);

            return viewModels;
        }

        public IEnumerable<ProductViewModel> GetAllFarmProducts(int farmId)
        {
            IEnumerable<Product> products = this.db.Products
                .All()
                .Where(p => p.IsDeleted == false 
                    && p.Category.IsDeleted == false 
                    && p.Owner.IsDeleted == false
                    && p.Owner.Id == farmId)
                .OrderBy(p => p.Id)
                .ToList();
            IEnumerable<ProductViewModel> viewModels = this.mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);

            return viewModels;
        }

        public IEnumerable<ProductViewModel> GetFilteredProducts(string category)
        {
            IEnumerable<Product> products = this.db.Products.All().Where(p => p.Category.Name == category && p.IsDeleted == false && p.Category.IsDeleted == false && p.Owner.IsDeleted == false).OrderBy(p => p.Id).ToList();
            IEnumerable<ProductViewModel> viewModels = this.mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);
            return viewModels;
        }

        public IEnumerable<ProductViewModel> GetSearchedProducts(string product)
        {
            IEnumerable<Product> products = this.db.Products.All().Where(p => p.Name.Contains(product) && p.IsDeleted == false && p.Category.IsDeleted == false && p.Owner.IsDeleted == false).OrderBy(p => p.Id).ToList();
            IEnumerable<ProductViewModel> viewModels = this.mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);
            return viewModels;
        }

        public IEnumerable<ProductViewModel> GetProductsByFarm(string farm)
        {
            IEnumerable<Product> products = this.db.Products.All().Where(p => p.Owner.Name == farm && p.IsDeleted == false && p.Category.IsDeleted == false && p.Owner.IsDeleted == false).OrderBy(p => p.Id).ToList();
            IEnumerable<ProductViewModel> viewModels = this.mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);
            return viewModels;
        }

        public ProductViewModel GetAddProduct()
        {
            ProductViewModel model = new ProductViewModel()
            {
                Categories = GetCategories(),
                Farms = GetFarms()
            };

            return model;
        }

        public void CreateNewProduct(ProductBindingModel model)
        {
            Product? existingProduct = this.db.Products.All().FirstOrDefault(p => p.Name == model.Name && p.CategoryId == model.CategoryId && p.OwnerId == model.FarmId);

            if (existingProduct == null)
            {
                var product = new Product()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Unit = model.Unit,
                    Quantity = model.Quantity,
                    ImageUrl = model.ImageUrl,
                    CategoryId = model.CategoryId,
                    OwnerId = model.FarmId
                };

                this.db.Products.Add(product);
            }
            else
            {
                existingProduct.IsDeleted = false;
                existingProduct.Description = model.Description;
                existingProduct.Price = model.Price;
                existingProduct.Unit = model.Unit;
                existingProduct.Quantity = model.Quantity;
                existingProduct.ImageUrl = model.ImageUrl;
                existingProduct.CategoryId = model.CategoryId;
                existingProduct.OwnerId = model.FarmId;
            }

            this.db.SaveChanges();
        }

        public ProductViewModel? GetEditProduct(int id)
        {
            Product? product = this.db.Products.Find(id);

            if (product == null || product.IsDeleted == true)
            {
                return null;
            }

            ProductViewModel viewModel = new ProductViewModel()
            {
                Id = product.Id,
                Categories = GetCategoriesById(product.CategoryId),
                Farms = GetFarmsById(product.OwnerId),
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Name = product.Name,
                Price = product.Price,
                Unit = product.Unit,
                Quantity = product.Quantity
            };

            return viewModel;
        }

        public void EditProduct(ProductBindingModel model)
        {
            Product? product = db.Products.Find(model.Id);

            if (product != null)
            {
                product.Name = model.Name;
                product.Description = model.Description;
                product.ImageUrl = model.ImageUrl;
                product.Price = model.Price;
                product.Unit = model.Unit;
                product.Quantity = model.Quantity;
                product.CategoryId = model.CategoryId;
                product.OwnerId = model.FarmId;

                this.db.SaveChanges();
            }
        }

        public ProductViewModel? GetDeleteProduct(int? id)
        {
            Product? product = this.db.Products.Find(id);

            if (product == null || product.IsDeleted == true)
            {
                return null;
            }

            ProductViewModel viewModel = new ProductViewModel()
            {
                Id = product.Id,
                Categories = GetCategoriesById(product.CategoryId),
                Farms = GetFarmsById(product.OwnerId),
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Name = product.Name,
                Price = product.Price,
                Unit = product.Unit,
                Quantity = product.Quantity
            };

            return viewModel;
        }

        public void DeleteProduct(int id)
        {
            Product? product = this.db.Products.Find(id);
            if (product == null) return;
            product.IsDeleted = true;
            this.db.SaveChanges();
        }

        private IEnumerable<SelectListItem> GetCategories()
        {
            var categories = this.db
                        .Categories
                        .All()
                        .Where(c => c.IsDeleted == false)
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name
                                }
                        )
                        .ToList();

            return new SelectList(categories, "Value", "Text");
        }

        private IEnumerable<SelectListItem> GetCategoriesById(int id)
        {
            var categories = this.db
                        .Categories
                        .All()
                        .Where(c => c.IsDeleted == false)
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name,
                                    Selected = x.Id == id
                                }
                        )
                        .ToList();

            return new SelectList(categories, "Value", "Text", id);
        }

        private IEnumerable<SelectListItem> GetFarms()
        {
            var farms = this.db
                        .Farms
                        .All()
                        .Where(c => c.IsDeleted == false)
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name
                                }
                        )
                        .ToList();

            return new SelectList(farms, "Value", "Text");
        }

        private IEnumerable<SelectListItem> GetFarmsById(int id)
        {
            var farms = this.db
                        .Farms
                        .All()
                        .Where(c => c.IsDeleted == false)
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name,
                                    Selected = x.Id == id
                                }
                        )
                        .ToList();

            return new SelectList(farms, "Value", "Text", id);
        }
    }
}
