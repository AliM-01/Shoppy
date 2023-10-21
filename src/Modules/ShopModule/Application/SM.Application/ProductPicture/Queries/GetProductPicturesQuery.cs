using FluentValidation;
using SM.Application.ProductPicture.DTOs;

namespace SM.Application.ProductPicture.Queries;

public record GetProductPicturesQuery(string ProductId) : IRequest<IEnumerable<ProductPictureDto>>;

public class GetProductPicturesQueryValidator : AbstractValidator<GetProductPicturesQuery>
{
    public GetProductPicturesQueryValidator()
    {
        RuleFor(p => p.ProductId)
            .RequiredValidator("شناسه محصول");
    }
}

public class GetProductPicturesQueryHandler : IRequestHandler<GetProductPicturesQuery, IEnumerable<ProductPictureDto>>
{
    private readonly IRepository<Domain.ProductPicture.ProductPicture> _productPictureRepository;
    private readonly IRepository<Domain.Product.Product> _productRepository;
    private readonly IMapper _mapper;

    public GetProductPicturesQueryHandler(IRepository<Domain.ProductPicture.ProductPicture> productPictureRepository,
        IRepository<Domain.Product.Product> productRepository, IMapper mapper)
    {
        _productPictureRepository = Guard.Against.Null(productPictureRepository, nameof(_productPictureRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productPictureRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<IEnumerable<ProductPictureDto>> Handle(GetProductPicturesQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(request.ProductId);

        NotFoundApiException.ThrowIfNull(product);

        bool anyProductPictures = await _productPictureRepository.ExistsAsync(p => p.ProductId == request.ProductId);

        if (!anyProductPictures)
            throw new NoContentApiException();

        var productPictures = _productPictureRepository.AsQueryable()
                                                       .Where(p => p.ProductId == request.ProductId)
                                                       .OrderBy(p => p.CreationDate)
                                                       .ToList()
                                                       .Select(productPicture => _mapper.Map(productPicture, new ProductPictureDto()));

        return productPictures;
    }
}