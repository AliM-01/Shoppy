namespace SM.Application.Contracts.ProductCategory.Commands;
public class DeleteProductCategoryCommand : IRequest<Response<string>>
{
    public DeleteProductCategoryCommand(long productCategoryId)
    {
        ProductCategoryId = productCategoryId;
    }

    public long ProductCategoryId { get; set; }
}