using _01_Shoppy.Query.Models.Discount;

namespace _01_Shoppy.Query.Queries.Discount;

public record CheckDiscountCodeQuery(string Code) : IRequest<ApiResult<CheckDiscountCodeResponseDto>>;

public class CheckDiscountCodeQueryHandler : IRequestHandler<CheckDiscountCodeQuery, ApiResult<CheckDiscountCodeResponseDto>>
{
    #region Ctor

    private readonly IRepository<CM.Domain.Comment.Comment> _commentRepository;
    private readonly IMapper _mapper;

    public CheckDiscountCodeQueryHandler(
        IRepository<CM.Domain.Comment.Comment> commentRepository, IMapper mapper)
    {
        _commentRepository = Guard.Against.Null(commentRepository, nameof(_commentRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult<CheckDiscountCodeResponseDto>> Handle(CheckDiscountCodeQuery request, CancellationToken cancellationToken)
    {
        return ApiResponse.Success<CheckDiscountCodeResponseDto>();
    }
}
