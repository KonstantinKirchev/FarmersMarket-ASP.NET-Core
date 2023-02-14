namespace FarmersMarket.Tests.Controllers
{
    using AutoMapper;
    using FarmersMarket.Data.UnitOfWork;
    using FarmersMarket.Models.BindingModels;
    using FarmersMarket.Models.EntityModels;
    using FarmersMarket.Models.ViewModels;
    using FarmersMarket.Services.Implementations;
    using FarmersMarket.Services.Interfaces;
    using FarmersMarket.Web.Areas.Admin.Controllers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class CategoriesControllerTest
    {
        private CategoriesController _controller;
        private MockContainer mocks;
        private Mock<IFarmersMarketData> dbMock;
        private ICategoriesService _service;
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

            _service = new CategoriesService(this.dbMock.Object, this.mapper);
            _controller = new CategoriesController(this._service);
        }

        [Fact]
        public void Index_ShouldPass()
        {
            // Arrange
            this.Init();

            // Act
            var result = _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Index_ShouldPassCorrectModelToTheView()
        {
            // Arrange
            this.Init();

            // Act
            var result = _controller.Index() as ViewResult;
            var categories = result.Model as IEnumerable<CategoryViewModel>;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(categories);
            Assert.Equal(3, categories.Count());
            Assert.IsType<ViewResult>(result);
            Assert.IsType<List<CategoryViewModel>>(categories);
        }

        [Fact]
        public void Create_ShouldPass()
        {
            // Arrange
            this.Init();

            // Act
            var result = _controller.Create() as ViewResult;

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void CreateNullModel_ShouldReturnDefaultView()
        {
            // Arrange
            this.Init();
            CategoryBindingModel model = null;

            // Act
            var result = _controller.Create(model);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void CreateValidModel_ShouldRedirectToIndex()
        {
            // Arrange
            this.Init();
            CategoryBindingModel model = new CategoryBindingModel()
            {
                Id = 4,
                Name = "Chushki"
            };

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SuccessMessage"] = "";
            _controller.TempData = tempData;

            // Act
            var result = _controller.Create(model) as ActionResult;

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void EditValidId_ShouldPass()
        {
            // Arrange
            this.Init();

            // Act
            var result = _controller.Edit(1) as ViewResult;

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void EditInValidId_ShouldReturnNotFound()
        {
            // Arrange
            this.Init();

            // Act
            var result = _controller.Edit(4) as StatusCodeResult;

            // Assert
            Assert.Equal("404", result.StatusCode.ToString());
        }

        [Fact]
        public void EditNullModel_ShouldReturnDefaultView()
        {
            // Arrange
            this.Init();
            CategoryBindingModel model = null;

            // Act
            var result = _controller.Edit(model);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void EditValidModel_ShouldRedirectToIndex()
        {
            // Arrange
            this.Init();

            // Act
            CategoryBindingModel model = new CategoryBindingModel()
            {
                Id = 3,
                Name = "Chushki"
            };

            var result = _controller.Edit(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void DeleteInValidId_ShouldReturnNotFound()
        {
            // Arrange
            this.Init();

            // Act
            var result = _controller.Delete(4) as ActionResult;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteValidId_ShouldRenderDeleteView()
        {
            // Arrange
            this.Init();

            // Act
            var result = _controller.Delete(2);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void DeleteValidId_ShouldPassCorrectModelToTheView()
        {
            // Arrange
            this.Init();

            // Act
            var result = _controller.Delete(2) as ViewResult;
            var category = result.Model as CategoryViewModel;

            // Assert
            Assert.IsType<ViewResult>(result);
            Assert.IsType<CategoryViewModel>(category);
            Assert.Equal("Vegetables", category.Name);
        }

        [Fact]
        public void DeleteValidId_ShouldRedirectToIndex()
        {
            // Arrange
            this.Init();

            // Act
            var result = _controller.DeleteConfirmed(2);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
