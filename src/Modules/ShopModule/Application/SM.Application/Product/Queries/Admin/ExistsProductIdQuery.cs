using _0_Framework.Domain.Validators;
using FluentValidation;
using SM.Application.Product.DTOs.Admin;

namespace SM.Application.Product.Queries.Admin;

public record ExistsProductIdQuery(string ProductId) : IRequest<ExistsProductIdResponseDto>;

public class ExistsProductIdQueryValidator : AbstractValidator<ExistsProductIdQuery>
{
    public ExistsProductIdQueryValidator()
    {
        RuleFor(p => p.ProductId)
            .RequiredValidator("شناسه");
    }
}


public class ExistsProductIdQueryHandler : IRequestHandler<ExistsProductIdQuery, ExistsProductIdResponseDto>
{
    private readonly IRepository<Domain.Product.Product> _productRepository;
    private readonly IMapper _mapper;

    public ExistsProductIdQueryHandler(IRepository<Domain.Product.Product> productRepository, IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<ExistsProductIdResponseDto> Handle(ExistsProductIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(request.ProductId);

        var response = new ExistsProductIdResponseDto
        {
            Exists = product is not null,
            ProductId = product is null ? "0" : product.Id,
            ProductTitle = product is null ? "" : product.Title
        };

        return response;
    }
}