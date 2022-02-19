﻿

namespace CM.Application.Comment.CommandHandles;

public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, Response<string>>
{
    #region Ctor

    private readonly IMongoHelper<Domain.Comment.Comment> _commentHelper;
    private readonly IMapper _mapper;

    public AddCommentCommandHandler(IMongoHelper<Domain.Comment.Comment> commentHelper, IMapper mapper)
    {
        _commentHelper = Guard.Against.Null(commentHelper, nameof(_commentHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = _mapper.Map(request.Comment, new Domain.Comment.Comment());

        if (string.IsNullOrEmpty(comment.ParentId))
            comment.ParentId = null;

        await _commentHelper.InsertAsync(comment);

        return new Response<string>("کامنت با موفقیت ثبت شد و پس از تایید توسط ادمین در سایت نمایش داده خواهد شد");
    }
}

