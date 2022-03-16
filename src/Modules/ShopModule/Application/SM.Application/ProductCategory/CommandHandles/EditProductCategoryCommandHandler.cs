using _0_Framework.Application.Extensions;
using SM.Application.Contracts.ProductCategory.Commands;
using System.IO;

namespace SM.Application.ProductCategory.CommandHandles;

public class EditProductCategoryCommandHandler : IRequestHandler<EditProductCategoryCommand, Response<string>>
{
    #region Ctor

    private readonly IRepository<Domain.ProductCategory.ProductCategory> _productCategoryRepository;
    private readonly IMapper _mapper;

    public EditProductCategoryCommandHandler(IRepository<Domain.ProductCategory.ProductCategory> productCategoryRepository, IMapper mapper)
    {
        _productCategoryRepository = Guard.Against.Null(productCategoryRepository, nameof(_productCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(EditProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var productCategory = await _productCategoryRepository.GetByIdAsync(request.ProductCategory.Id);

        if (productCategory is null)
            throw new NotFoundApiException();

        if (await _productCategoryRepository.ExistsAsync(x => x.Title == request.ProductCategory.Title && x.Id != request.ProductCategory.Id))
            throw new ApiException(ApplicationErrorMessage.DuplicatedRecordExists);

        _mapper.Map(request.ProductCategory, productCategory);

        if (request.ProductCategory.ImageFile != null)
        {
            var imagePath = DateTime.Now.ToFileName() + Path.GetExtension(request.ProductCategory.ImageFile.FileName);

            request.ProductCategory.ImageFile.AddImageToServer(imagePath, PathExtension.ProductCategoryImage,
                200, 200, PathExtension.ProductCategoryThumbnailImage, productCategory.ImagePath);

            productCategory.ImagePath = imagePath;
        }

        await _productCategoryRepository.UpdateAsync(productCategory);

        return new Response<string>();
    }
}