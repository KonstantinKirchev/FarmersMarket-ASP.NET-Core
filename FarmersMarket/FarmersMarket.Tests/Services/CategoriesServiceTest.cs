namespace FarmersMarket.Tests.Services
{
    using AutoMapper;
    using FarmersMarket.Data;
    using FarmersMarket.Data.UnitOfWork;
    using FarmersMarket.Models.BindingModels;
    using FarmersMarket.Models.EntityModels;
    using FarmersMarket.Models.ViewModels;
    using FarmersMarket.Services.Implementations;
    using FarmersMarket.Services.Interfaces;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using System;
    using System.Linq;
    using Xunit;

    public class CategoriesServiceTest
    {
        private MockContainer mocks;
        private Mock<IFarmersMarketData> dbMock;
        private ICategoriesService service;
        private IMapper mapper;

        private void ConfigureMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryViewModel>();
                cfg.CreateMap<CategoryBindingModel, Category>();
            });

            this.mapper = config.CreateMapper();
        }

        public void Init()
        {
            this.ConfigureMapper();

            this.dbMock = new Mock<IFarmersMarketData>();
            this.mocks = new MockContainer();
            this.mocks.PrepareMocks();

            this.dbMock.Setup(c => c.Categories).Returns(this.mocks.CategoryRepositoryMock.Object);
            this.service = new CategoriesService(this.dbMock.Object, this.mapper);
        }

        [Fact]
        public void GetAllCategories_ShouldReturnGivenCategories()
        {
            // Arrange
            this.Init();
            
            // Act
            var categories = this.service.GetAllCategories();
            
            // Assert
            Assert.NotNull(categories);
            Assert.Equal(3, categories.Count());
            Assert.Equal("Fruits", categories.FirstOrDefault().Name);
        }

        [Fact]
        public void GetEditCategoryWithValidId_ShouldReturnGivenCategory()
        {
            // Arrange
            this.Init();

            // Act
            var category = this.service.GetEditCategory(1);

            // Assert
            Assert.NotNull(category);
            Assert.Equal(1, category.Id);
            Assert.Equal("Fruits", category.Name);
        }

        [Fact]
        public void GetEditCategoryWithInvalidId_ShouldReturnNull()
        {
            // Arrange
            this.Init();

            // Act
            var category = this.service.GetEditCategory(4);

            // Assert
            Assert.Null(category);
        }

        [Fact]
        public void GetDeleteCategoryWithValidId_ShouldReturnGivenCategory()
        {
            // Arrange
            this.Init();

            // Act
            var category = this.service.GetDeleteCategory(2);

            // Assert
            Assert.NotNull(category);
            Assert.Equal(2, category.Id);
            Assert.Equal("Vegetables", category.Name);
        }

        [Fact]
        public void GetDeleteCategoryWithInvalidId_ShouldReturnNull()
        {
            // Arrange
            this.Init();

            // Act
            var category = this.service.GetDeleteCategory(4);

            // Assert
            Assert.Null(category);
        }

        [Fact]
        public void PostValidCategory_ShouldAddToRepo()
        {
            // Arrange
            this.Init();

            // Act
            CategoryBindingModel testCategory = new CategoryBindingModel() { Name = "Meats" };
            this.service.CreateNewCategory(testCategory);

            // Assert
            Assert.Equal(4, this.dbMock.Object.Categories.All().Count());
        }

        [Fact]
        public void PutValidCategory_ShouldModifyExistingCategory()
        {
            // Arrange
            this.Init();

            // Act
            CategoryBindingModel testCategory = new CategoryBindingModel() { Id = 1, Name = "Fish" };
            this.service.EditCategory(testCategory);

            // Assert
            Assert.Equal("Fish", this.dbMock.Object.Categories.Find(1).Name);
        }

        [Fact]
        public void DeleteExistingCategory_ShouldDeleteCategoryFromRepo()
        {
            // Arrange
            this.Init();

            // Act
            this.service.DeleteCategory(2);
            var category = this.dbMock.Object.Categories.Find(2);

            // Assert
            Assert.True(category.IsDeleted);
            Assert.Equal(3, this.dbMock.Object.Categories.All().Count());
        }

        [Fact]
        public void DeleteNotExistingCategory_ShouldNotDeleteCategoryFromRepo()
        {
            // Arrange
            this.Init();

            // Act
            this.service.DeleteCategory(4);

            // Assert
            Assert.Equal(3, this.dbMock.Object.Categories.All().Count());
        }
    }
}
