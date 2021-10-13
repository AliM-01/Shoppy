using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application.ErrorMessages;
using _0_Framework.Application.Extensions;
using _0_Framework.Application.Wrappers;
using SM.Application.Contracts.ProductCategory.Interfaces;
using SM.Application.Contracts.ProductCategory.Models;
using SM.Domain.ProductCategoryAgg;

namespace SM.Application.ProductCategory
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        #region Ctor

        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        #endregion

        #region Filter

        public async Task<OperationResult<List<ProductCategoryViewModel>>> Filter(FilterProductCategoryModel filter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region GetDetails

        public async Task<OperationResult<Domain.ProductCategoryAgg.ProductCategory>> GetDetails(long id)
        {
            var operation = new OperationResult<Domain.ProductCategoryAgg.ProductCategory>();

            var productCategory = await _productCategoryRepository.GetByIdAsync(id);

            if (productCategory is null)
                return operation.Failed(ApplicationErrorMessage.RecordNotFoundMessage);

            return new OperationResult<Domain.ProductCategoryAgg.ProductCategory>().Succedded(productCategory);
        }

        #endregion

        #region Create

        public async Task<OperationResult> Create(CreateProductCategoryModel command)
        {
            var operation = new OperationResult();

            if (await _productCategoryRepository.ExistsAsync(x => x.Title == command.Title))
                return operation.Failed(ApplicationErrorMessage.IsDuplicatedMessage);

            var slug = command.Title.ToSlug();

            var productCategory = new Domain.ProductCategoryAgg.ProductCategory(command.Title, command.Description,
                command.ImagePath, command.ImageAlt, command.ImageTitle,
                command.MetaKeywords, command.MetaDescription, slug);

            await _productCategoryRepository.CreateAsync(productCategory);
            await _productCategoryRepository.SaveChangesAsync();

            return operation.Succedded();
        }

        #endregion

        #region Edit

        public async Task<OperationResult> Edit(EditProductCategoryModel command)
        {
            var operation = new OperationResult();
            var productCategory = await _productCategoryRepository.GetByIdAsync(command.Id);

            if (productCategory is null)
                return operation.Failed(ApplicationErrorMessage.RecordNotFoundMessage);

            if (await _productCategoryRepository.ExistsAsync(x => x.Title == command.Title && x.Id != command.Id))
                return operation.Failed(ApplicationErrorMessage.IsDuplicatedMessage);

            var slug = command.Title.ToSlug();

            productCategory.Edit(command.Title, command.Description,
                command.ImagePath, command.ImageAlt, command.ImageTitle,
                command.MetaKeywords, command.MetaDescription, slug);

            await _productCategoryRepository.SaveChangesAsync();

            return operation.Succedded();
        }

        #endregion
    }
}