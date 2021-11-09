
using _0_Framework.Application.Utilities.ImageRelated;
using AutoMapper;
using SM.Application.Contracts.ProductCategory.Commands;
using System.IO;

namespace SM.Application.ProductCategory.CommandHandles;

public class CreateProductCategoryCommandHandler : IRequestHandler<CreateProductCategoryCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ProductCategory.ProductCategory> _productCategoryRepository;
    private readonly IMapper _mapper;

    public CreateProductCategoryCommandHandler(IGenericRepository<Domain.ProductCategory.ProductCategory> productCategoryRepository, IMapper mapper)
    {
        _productCategoryRepository = Guard.Against.Null(productCategoryRepository, nameof(_productCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        if (_productCategoryRepository.Exists(x => x.Title == request.ProductCategory.Title))
            throw new ApiException(ApplicationErrorMessage.IsDuplicatedMessage);

        var productCategory =
            _mapper.Map(request.ProductCategory, new Domain.ProductCategory.ProductCategory());

        var imagePath = Guid.NewGuid().ToString("N") + Path.GetExtension(request.ProductCategory.ImageFile.FileName);

        request.ProductCategory.ImageFile.AddImageToServer(imagePath, "wwwroot/product_category/original/", 200, 200, "wwwroot/product_category/thumbnail/");
        productCategory.ImagePath = imagePath;

        await _productCategoryRepository.InsertEntity(productCategory);
        await _productCategoryRepository.SaveChanges();

        return new Response<string>(ApplicationErrorMessage.OperationSucceddedMessage);
    }
}