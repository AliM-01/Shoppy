using BM.Application.Contracts.ArticleCategory.DTOs;

namespace BM.Application.Contracts.ArticleCategory.Commands;

public record EditArticleCategoryCommand
    (EditArticleCategoryDto ArticleCategory) : IRequest<Response<string>>;