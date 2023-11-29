using _0_Framework.Domain.Validators;
using FluentValidation;
using SM.Application.Product.DTOs.Admin;

namespace SM.Application.Product.Queries.Admin;

public record GetProductDetailsQuery(string Id) : IRequest<EditProductDto>;

public class GetProductDetailsQueryValidator : AbstractValidator<GetProductDetailsQuery>
{
    public GetProductDetailsQueryValidator()
    {
        RuleFor(p => p.Id)
            .RequiredValidator("شناسه");
    }
}

public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, EditProductDto>
{
    private readonly IRepository<Domain.Product.Product> _productRepository;
    private readonly IMapper _mapper;

    public GetProductDetailsQueryHandler(IRepository<Domain.Product.Product> productRepository, IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<EditProductDto> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(request.Id);

        NotFoundApiException.ThrowIfNull(product);

        return _mapper.Map<EditProductDto>(product);
    }
}