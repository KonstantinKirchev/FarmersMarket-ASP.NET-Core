namespace FarmersMarket.Services.Interfaces
{
    using FarmersMarket.Models.BindingModels;
    using FarmersMarket.Models.ViewModels;

    public interface IProductsService
    {
        IEnumerable<ProductViewModel> GetAllProducts();
        IEnumerable<ProductViewModel> GetFilteredProducts(string category);
        IEnumerable<ProductViewModel> GetSearchedProducts(string product);
        IEnumerable<ProductViewModel> GetProductsByFarm(string farm);
        ProductViewModel GetAddProduct();
        void CreateNewProduct(ProductBindingModel model);
        ProductViewModel? GetEditProduct(int id);
        void EditProduct(ProductBindingModel model);
        ProductViewModel? GetDeleteProduct(int? id);
        void DeleteProduct(int id);
    }
}
