using SM.Application.Contracts.ProductPicture.DTOs;
using System.Collections.Generic;

namespace SM.Application.Contracts.ProductPicture.Queries;

public record GetProductPicturesQuery
    (long ProductId) : IRequest<Response<IEnumerable<ProductPictureDto>>>;