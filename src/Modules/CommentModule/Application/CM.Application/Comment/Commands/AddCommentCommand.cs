using CM.Application.Comment.DTOs;
using FluentValidation;

namespace CM.Application.Comment.Commands;

public record AddCommentCommand(AddCommentDto Comment) : IRequest<ApiResult>;

public class AddCommentCommandValidator : AbstractValidator<AddCommentCommand>
{
    public AddCommentCommandValidator()
    {
        RuleFor(p => p.Comment.Name)
            .RequiredValidator("نام");

        RuleFor(p => p.Comment.Email)
            .CustomEmailAddressValidator();

        RuleFor(p => p.Comment.Text)
            .RequiredValidator("نام")
            .MaxLengthValidator("متن نظر", 500);

        RuleFor(p => p.Comment.OwnerRecordId)
            .RequiredValidator("شناسه محصول/مقاله");

        RuleFor(p => p.Comment.ParentId)
            .RequiredValidator("شناسه والد");
    }
}

public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, ApiResult>
{
    private readonly IRepository<Domain.Comment.Comment> _commentRepository;
    private readonly IMapper _mapper;

    public AddCommentCommandHandler(IRepository<Domain.Comment.Comment> commentRepository, IMapper mapper)
    {
        _commentRepository = Guard.Against.Null(commentRepository, nameof(_commentRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<ApiResult> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = _mapper.Map(request.Comment, new Domain.Comment.Comment());

        if (string.IsNullOrEmpty(comment.ParentId))
            comment.ParentId = null;

        await _commentRepository.InsertAsync(comment);

        return ApiResponse.Success("کامنت با موفقیت ثبت شد و پس از تایید توسط ادمین در سایت نمایش داده خواهد شد");
    }
}

