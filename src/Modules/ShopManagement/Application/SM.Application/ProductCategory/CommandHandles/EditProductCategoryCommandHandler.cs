using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application.ErrorMessages;
using _0_Framework.Application.Exceptions;
using _0_Framework.Application.Utilities.ImageRelated;
using _0_Framework.Application.Wrappers;
using _0_Framework.Domain.IGenericRepository;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using SM.Application.Contracts.ProductCategory.Commands;

namespace SM.Application.ProductCategory.CommandHandles
{
    public class EditProductCategoryCommandHandler : IRequestHandler<EditProductCategoryCommand, Response<string>>
    {
        #region Ctor

        private readonly IGenericRepository<Domain.ProductCategory.ProductCategory> _productCategoryRepository;
        private readonly IMapper _mapper;

        public EditProductCategoryCommandHandler(IGenericRepository<Domain.ProductCategory.ProductCategory> productCategoryRepository, IMapper mapper)
        {
            _productCategoryRepository = Guard.Against.Null(productCategoryRepository, nameof(_productCategoryRepository));
            _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        }

        #endregion

        public async Task<Response<string>> Handle(EditProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var productCategory = await _productCategoryRepository.GetEntityById(request.ProductCategory.Id);

            if (productCategory is null)
                throw new NotFoundApiException();

            if (_productCategoryRepository.Exists(x => x.Title == request.ProductCategory.Title && x.Id != request.ProductCategory.Id))
                throw new ApiException(ApplicationErrorMessage.IsDuplicatedMessage);

            _mapper.Map(request, productCategory);

            var imagePath = Guid.NewGuid().ToString("N") + Path.GetExtension(request.ProductCategory.ImageFile.FileName);

            request.ProductCategory.ImageFile.AddImageToServer(imagePath, "wwwroot/product_category/original/", 200, 200, "wwwroot/product_category/thumbnail/", productCategory.ImagePath);
            productCategory.ImagePath = imagePath;

            _productCategoryRepository.Update(productCategory);
            await _productCategoryRepository.SaveChanges();

            return new Response<string>();
        }
    }
}