using _0_Framework.Application.Utilities.ImageRelated;
using AutoMapper;
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
        var Product = await _productRepository.GetQuery().Include(p => p.Category)
            .AsNoTracking().FirstOrDefaultAsync(s => s.Id == request.Product.Id);

        if (Product is null)
            throw new NotFoundApiException();

        if (_productRepository.Exists(x => x.Title == request.Product.Title && x.Id != request.Product.Id))
            throw new ApiException(ApplicationErrorMessage.IsDuplicatedMessage);

        _mapper.Map(request, Product);

        if (request.Product.ImageFile != null)
        {
            var imagePath = Guid.NewGuid().ToString("N") + Path.GetExtension(request.Product.ImageFile.FileName);

            request.Product.ImageFile.AddImageToServer(imagePath, "wwwroot/product/original/", 200, 200, "wwwroot/product/thumbnail/", Product.ImagePath);
            Product.ImagePath = imagePath;
        }

        _productRepository.Update(Product);
        await _productRepository.SaveChanges();

        return new Response<string>();
    }
}