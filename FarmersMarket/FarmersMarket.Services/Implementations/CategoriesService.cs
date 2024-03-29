﻿namespace FarmersMarket.Services.Implementations
{
    using AutoMapper;
    using FarmersMarket.Data.UnitOfWork;
    using FarmersMarket.Models.BindingModels;
    using FarmersMarket.Models.EntityModels;
    using FarmersMarket.Models.ViewModels;
    using FarmersMarket.Services.Interfaces;

    public class CategoriesService : Service, ICategoriesService
    {
        public CategoriesService(IFarmersMarketData db, IMapper mapper) 
            : base(db, mapper)
        {
        }

        public IEnumerable<CategoryViewModel> GetAllCategories()
        {
            IEnumerable<Category> categories = this.db.Categories.All().Where(c => c.IsDeleted == false).OrderBy(c => c.Id).ToList();
            IEnumerable<CategoryViewModel> viewModels = this.mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categories);

            return viewModels;
        }
        public IEnumerable<CategoryViewModel> GetActiveCategories()
        {
            IEnumerable<Category> categories = this.db.Categories.All().Where(c => c.IsDeleted == false && c.Products.Any()).OrderBy(c => c.Id).ToList();
            IEnumerable<CategoryViewModel> viewModels = this.mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categories);

            return viewModels;
        }

        public void CreateNewCategory(CategoryBindingModel model)
        {
            Category? existingCategory = this.db.Categories.All().FirstOrDefault(c => c.Name == model.Name);

            if (existingCategory == null)
            {
                var category = new Category()
                {
                    Name = model.Name
                };

                this.db.Categories.Add(category);
            }
            else
            {
                existingCategory.IsDeleted = false;
            }

            this.db.SaveChanges();
        }

        public CategoryViewModel? GetEditCategory(int? id)
        {
            Category? category = this.db.Categories.Find(id);

            if (category == null || category.IsDeleted == true)
            {
                return null;
            }

            CategoryViewModel viewModel = this.mapper.Map<Category, CategoryViewModel>(category);

            return viewModel;
        }

        public void EditCategory(CategoryBindingModel model)
        {
            Category? category = this.db.Categories.Find(model.Id);

            if (category != null && category.IsDeleted == false)
            {
                category.Name = model.Name;

                this.db.SaveChanges();
            }
            
        }

        public CategoryViewModel? GetDeleteCategory(int? id)
        {
            Category? categeory = this.db.Categories.Find(id);

            if (categeory == null || categeory.IsDeleted == true)
            {
                return null;
            }

            CategoryViewModel viewModel = this.mapper.Map<Category, CategoryViewModel>(categeory);

            return viewModel;
        }

        public void DeleteCategory(int id)
        {
            Category? category = this.db.Categories.Find(id);
            if (category == null) return;
            category.IsDeleted = true;
            this.db.SaveChanges();
        }
    }
}
