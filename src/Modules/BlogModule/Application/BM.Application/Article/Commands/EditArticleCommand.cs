using BM.Application.Article.DTOs;
using FluentValidation;

namespace BM.Application.Article.Commands;

public record EditArticleCommand(EditArticleDto Article) : IRequest<ApiResult>;

public class EditArticleCommandValidator : AbstractValidator<EditArticleCommand>
{
    public EditArticleCommandValidator()
    {
        RuleFor(p => p.Article.Id)
            .RequiredValidator("شناسه");

        RuleFor(p => p.Article.Title)
            .RequiredValidator("عنوان");

        RuleFor(p => p.Article.Text)
            .RequiredValidator("متن");

        RuleFor(p => p.Article.Summary)
            .RequiredValidator("توضیحات کوتاه")
            .MaxLengthValidator("توضیحات کوتاه", 250);

        RuleFor(p => p.Article.ImageFile)
            .MaxFileSizeValidator((3 * 1024 * 1024), false);
    }
}

public class EditArticleCommandHandler : IRequestHandler<EditArticleCommand, ApiResult>
{
    private readonly IRepository<Domain.Article.Article> _articleRepository;
    private readonly IMapper _mapper;

    public EditArticleCommandHandler(IRepository<Domain.Article.Article> articleRepository, IMapper mapper)
    {
        _articleRepository = Guard.Against.Null(articleRepository, nameof(_articleRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<ApiResult> Handle(EditArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.FindByIdAsync(request.Article.Id);

        NotFoundApiException.ThrowIfNull(article);

        if (await _articleRepository.ExistsAsync(x => x.Title == request.Article.Title && x.Id != request.Article.Id))
            throw new ApiException(ApplicationErrorMessage.DuplicatedRecordExists);

        _mapper.Map(request.Article, article);

        if (request.Article.ImageFile != null)
        {
            string imagePath = request.Article.ImageFile.GenerateImagePath();

            request.Article.ImageFile.AddImageToServer(imagePath, PathExtension.ArticleImage,
                200, 200, PathExtension.ArticleThumbnailImage, article.ImagePath);

            article.ImagePath = imagePath;
        }

        await _articleRepository.UpdateAsync(article);

        return ApiResponse.Success();
    }
}