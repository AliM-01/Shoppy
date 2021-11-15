using SM.Application.Contracts.ProductPicture.DTOs;

namespace SM.Application.Contracts.ProductPicture.Queries;

public class FilterProductPicturesQuery : IRequest<Response<FilterProductPictureDto>>
{
    public FilterProductPicturesQuery(FilterProductPictureDto filter)
    {
        Filter = filter;
    }

    public FilterProductPictureDto Filter { get; set; }
}