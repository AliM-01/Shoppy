using BM.Application.Article.DTOs;
using FluentValidation;

namespace BM.Application.Article.Commands;

public record CreateArticleCommand(CreateArticleRequest Article) : IRequest<ApiResult>;

public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
    public CreateArticleCommandValidator()
    {
        RuleFor(p => p.Article.Title)
            .RequiredValidator("عنوان");

        RuleFor(p => p.Article.Text)
            .RequiredValidator("متن");

        RuleFor(p => p.Article.Summary)
            .RequiredValidator("توضیحات کوتاه")
            .MaxLengthValidator("توضیحات کوتاه", 250);

        RuleFor(p => p.Article.ImageFile)
            .MaxFileSizeValidator((3 * 1024 * 1024));
    }
}


public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, ApiResult>
{
    private readonly IRepository<Domain.Article.Article> _articleRepository;
    private readonly IMapper _mapper;

    public CreateArticleCommandHandler(IRepository<Domain.Article.Article> articleRepository, IMapper mapper)
    {
        _articleRepository = Guard.Against.Null(articleRepository, nameof(_articleRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<ApiResult> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        if (await _articleRepository.ExistsAsync(x => x.Title == request.Article.Title))
            throw new ApiException(ApplicationErrorMessage.DuplicatedRecordExists);

        var article = _mapper.Map(request.Article, new Domain.Article.Article());

        string imagePath = request.Article.ImageFile.GenerateImagePath();

        request.Article.ImageFile.AddImageToServer(imagePath, PathExtension.ArticleImage,
                    200, 200, PathExtension.ArticleThumbnailImage);

        article.ImagePath = imagePath;

        await _articleRepository.InsertAsync(article);

        return ApiResponse.Success();
    }
}