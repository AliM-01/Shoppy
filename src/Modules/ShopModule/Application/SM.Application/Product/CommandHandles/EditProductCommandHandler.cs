using _0_Framework.Application.Utilities.ImageRelated;
using Microsoft.EntityFrameworkCore;
using SM.Application.Contracts.Product.Commands;
using System.IO;

namespace SM.Application.Product.CommandHandles;

public class EditProductCommandHandler : IRequestHandler<EditProductCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Product.Product> _productRepository;
    private readonly IMapper _mapper;

    public EditProductCommandHandler(IGenericRepository<Domain.Product.Product> productRepository, IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetQuery()
            .Include(p => p.Category)
            .FirstOrDefaultAsync(s => s.Id == request.Product.Id);

        if (product is null)
            throw new NotFoundApiException();

        if (_productRepository.Exists(x => x.Title == request.Product.Title && x.Id != request.Product.Id))
            throw new ApiException(ApplicationErrorMessage.IsDuplicatedMessage);

        _mapper.Map(request.Product, product);

        if (request.Product.ImageFile != null)
        {
            var imagePath = Path.GetExtension(request.Product.ImageFile.FileName);

            request.Product.ImageFile.AddImageToServer(imagePath, PathExtension.ProductImage, 200, 200, PathExtension.ProductThumbnailImage, product.ImagePath);
            product.ImagePath = imagePath;
        }

        product.CategoryId = request.Product.CategoryId;

        _productRepository.Update(product);
        await _productRepository.SaveChanges();

        return new Response<string>();
    }
}