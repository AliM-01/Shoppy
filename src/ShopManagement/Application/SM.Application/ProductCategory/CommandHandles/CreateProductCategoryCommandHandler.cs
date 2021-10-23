using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application.ErrorMessages;
using _0_Framework.Application.Exceptions;
using _0_Framework.Application.Wrappers;
using _0_Framework.Domain.IGenericRepository;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using SM.Application.Contracts.ProductCategory.Commands;

namespace SM.Application.ProductCategory.CommandHandles
{
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

            await _productCategoryRepository.InsertEntity(productCategory);
            await _productCategoryRepository.SaveChanges();

            return new Response<string>(ApplicationErrorMessage.OperationSucceddedMessage);
        }
    }
}