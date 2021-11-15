using SM.Application.Contracts.ProductPicture.DTOs;

namespace SM.Application.Contracts.ProductPicture.Queries;
public class GetProductPictureDetailsQuery : IRequest<Response<EditProductPictureDto>>
{
    public GetProductPictureDetailsQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}