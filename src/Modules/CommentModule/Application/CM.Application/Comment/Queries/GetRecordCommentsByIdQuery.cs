using AutoMapper;
using CM.Application.Comment.DTOs;
using CM.Domain.Comment;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;

namespace CM.Application.Comment.Queries;

public record GetRecordCommentsByIdQuery(string RecordId) : IRequest<IEnumerable<SiteCommentDto>>;

public class GetRecordCommentsByIdQueryHandler : IRequestHandler<GetRecordCommentsByIdQuery, IEnumerable<SiteCommentDto>>
{
    private readonly IRepository<Domain.Comment.Comment> _commentRepository;
    private readonly IMapper _mapper;

    public GetRecordCommentsByIdQueryHandler(
        IRepository<Domain.Comment.Comment> commentRepository, IMapper mapper)
    {
        _commentRepository = Guard.Against.Null(commentRepository, nameof(_commentRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public Task<IEnumerable<SiteCommentDto>> Handle(GetRecordCommentsByIdQuery request, CancellationToken cancellationToken)
    {
        var comments = _commentRepository
            .AsQueryable(cancellationToken: cancellationToken)
            .Where(x => x.ParentId == null)
            .Where(x => x.OwnerRecordId == request.RecordId && x.State == CommentState.Confirmed)
            .ToList()
            .OrderByDescending(x => x.LastUpdateDate)
            .Select(c =>
                _mapper.Map(c, new SiteCommentDto()))
            .ToList();

        for (int i = 0; i < comments.Count; i++)
        {
            var replies = _commentRepository
                .AsQueryable()
                .Where(x => x.ParentId == comments[i].Id.ToString())
                .ToList()
                .OrderByDescending(x => x.LastUpdateDate)
                .Select(c =>
                    _mapper.Map(c, new SiteCommentDto()))
                .ToArray();

            comments[i].Replies = replies;
        }

        return Task.FromResult((IEnumerable<SiteCommentDto>)comments);
    }
}
