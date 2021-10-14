using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application.ErrorMessages;
using _0_Framework.Application.Wrappers;
using _0_Framework.Domain.IGenericRepository;
using AutoMapper;
using SM.Application.Contracts.ProductCategory.Interfaces;
using SM.Application.Contracts.ProductCategory.Models;

namespace SM.Application.ProductCategory
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        #region Ctor

        private readonly IGenericRepository<Domain.ProductCategory.ProductCategory> _productCategoryRepository;
        private readonly IMapper _mapper;

        public ProductCategoryApplication(IGenericRepository<Domain.ProductCategory.ProductCategory> productCategoryRepository, IMapper mapper)
        {
            _productCategoryRepository = productCategoryRepository;
            _mapper = mapper;
        }

        #endregion

        #region Filter

        public async Task<OperationResult<List<ProductCategoryViewModel>>> Filter(FilterProductCategoryModel filter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region GetDetails

        public async Task<OperationResult<Domain.ProductCategory.ProductCategory>> GetDetails(long id)
        {
            var operation = new OperationResult<Domain.ProductCategory.ProductCategory>();

            var productCategory = await _productCategoryRepository.GetEntityById(id);

            if (productCategory is null)
                return operation.Failed(ApplicationErrorMessage.RecordNotFoundMessage);

            return new OperationResult<Domain.ProductCategory.ProductCategory>().Succedded(productCategory);
        }

        #endregion

        #region Create

        public async Task<OperationResult> Create(CreateProductCategoryModel command)
        {
            var operation = new OperationResult();

            if (_productCategoryRepository.Exists(x => x.Title == command.Title))
                return operation.Failed(ApplicationErrorMessage.IsDuplicatedMessage);

            var productCategory =
                _mapper.Map(command, new Domain.ProductCategory.ProductCategory());
            
            await _productCategoryRepository.InsertEntity(productCategory);
            await _productCategoryRepository.SaveChanges();

            return operation.Succedded();
        }

        #endregion

        #region Edit

        public async Task<OperationResult> Edit(EditProductCategoryModel command)
        {
            var operation = new OperationResult();
            var productCategory = await _productCategoryRepository.GetEntityById(command.Id);

            if (productCategory is null)
                return operation.Failed(ApplicationErrorMessage.RecordNotFoundMessage);

            if (_productCategoryRepository.Exists(x => x.Title == command.Title && x.Id != command.Id))
                return operation.Failed(ApplicationErrorMessage.IsDuplicatedMessage);

            _mapper.Map(command, productCategory);

            _productCategoryRepository.Update(productCategory);
            await _productCategoryRepository.SaveChanges();

            return operation.Succedded();
        }

        #endregion
    }
}