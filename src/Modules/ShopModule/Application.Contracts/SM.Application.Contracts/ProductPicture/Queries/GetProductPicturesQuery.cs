using SM.Application.ProductPicture.DTOs;
using System.Collections.Generic;

namespace SM.Application.ProductPicture.Queries;

public record GetProductPicturesQuery(string ProductId) : IRequest<IEnumerable<ProductPictureDto>>;