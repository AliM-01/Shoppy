using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application.ErrorMessages;
using _0_Framework.Application.Extensions;
using _0_Framework.Application.Wrappers;
using _0_Framework.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;
using SM.Application.Contracts.ProductCategory.Interfaces;
using SM.Application.Contracts.ProductCategory.Models;

namespace SM.Application.ProductCategory
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        #region Ctor

        private readonly IGenericRepository<Domain.ProductCategory.ProductCategory> _productCategoryRepository;

        public ProductCategoryApplication(IGenericRepository<Domain.ProductCategory.ProductCategory> productCategoryRepository)
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

            if (await _productCategoryRepository.GetQuery()
                .AnyAsync(x => x.Title == command.Title))
                return operation.Failed(ApplicationErrorMessage.IsDuplicatedMessage);

            var productCategory = new Domain.ProductCategory.ProductCategory
            {
                Title = command.Title,
                Description = command.Description,
                ImagePath = command.ImagePath,
                ImageAlt = command.ImageAlt,
                ImageTitle = command.ImageTitle,
                MetaKeywords = command.MetaKeywords,
                MetaDescription = command.MetaDescription,
                Slug = command.Title.ToSlug()
            };

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

            if (await _productCategoryRepository.GetQuery()
                .AnyAsync(x => x.Title == command.Title && x.Id != command.Id))
                return operation.Failed(ApplicationErrorMessage.IsDuplicatedMessage);

            productCategory.Title = command.Title;
            productCategory.Description = command.Description;
            productCategory.ImagePath = command.ImagePath;
            productCategory.ImageAlt = command.ImageAlt;
            productCategory.ImageTitle = command.ImageTitle;
            productCategory.MetaKeywords = command.MetaKeywords;
            productCategory.MetaDescription = command.MetaDescription;
            productCategory.Slug = command.Title.ToSlug();

            _productCategoryRepository.UpdateEntity(productCategory);
            await _productCategoryRepository.SaveChanges();

            return operation.Succedded();
        }

        #endregion
    }
}