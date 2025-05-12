using Mapster;
using ProductCatalog.Core.DTO;
using ProductCatalog.Data.DbModels;

namespace ProductCatalog.Mapper
{
    public class MapRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // map ShoppingCart -> DtoShoppingCart
            config.ForType<ShoppingCart, DtoShoppingCart>().
                Map(dest => dest.CustomerName, src => src.Customer.Name).
                Map(dest => dest.ProductName, src => src.Product.Name);

            config.ForType<DtoShoppingCartCreate, ShoppingCart>().
                Map(dest => dest.Customer.Id, src=>src.CustomerId );

            config.ForType<Product, DtoProduct>();

            config.ForType<DtoUserSignUp, User>();
            config.ForType<User, DtoUserSignUp>();
        }
    }
}
