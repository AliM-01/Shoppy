﻿using BM.Application.Contracts.ArticleCategory.Commands;

namespace BM.Application.ArticleCategory.CommandHandles;

public class EditArticleCategoryCommandHandler : IRequestHandler<EditArticleCategoryCommand, ApiResult>
{
    #region Ctor

    private readonly IRepository<Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IMapper _mapper;

    public EditArticleCategoryCommandHandler(IRepository<Domain.ArticleCategory.ArticleCategory> articleCategoryRepository, IMapper mapper)
    {
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult> Handle(EditArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        var articleCategory = await _articleCategoryRepository.FindByIdAsync(request.ArticleCategory.Id);

        if (articleCategory is null)
            throw new NotFoundApiException();

        if (await _articleCategoryRepository.ExistsAsync(x => x.Title == request.ArticleCategory.Title && x.Id != request.ArticleCategory.Id))
            throw new ApiException(ApplicationErrorMessage.DuplicatedRecordExists);

        _mapper.Map(request.ArticleCategory, articleCategory);

        if (request.ArticleCategory.ImageFile != null)
        {
            string imagePath = request.ArticleCategory.ImageFile.GenerateImagePath();

            request.ArticleCategory.ImageFile.CropAndAddImageToServer(imagePath, PathExtension.ArticleCategoryImage, 200, 200);

            articleCategory.ImagePath = imagePath;
        }

        await _articleCategoryRepository.UpdateAsync(articleCategory);

        return ApiResponse.Success();
    }
}