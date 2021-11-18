using SM.Application.Contracts.ProductPicture.DTOs;
using System.Collections.Generic;

namespace SM.Application.Contracts.ProductPicture.Queries;

public class GetProductPicturesQuery : IRequest<Response<IEnumerable<ProductPictureDto>>>
{
    public GetProductPicturesQuery(long productId)
    {
        ProductId = productId;
    }

    public long ProductId { get; set; }
}