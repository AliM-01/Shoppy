using DM.Application.Contracts.ColleagueDiscount.DTOs;
using DM.Application.Contracts.ColleagueDiscount.Queries;

namespace DM.Application.ColleagueDiscount.QueryHandles;
public class GetColleagueDiscountDetailsQueryHandler : IRequestHandler<GetColleagueDiscountDetailsQuery, Response<EditColleagueDiscountDto>>
{
    #region Ctor

    private readonly IMongoHelper<Domain.ColleagueDiscount.ColleagueDiscount> _colleagueDiscountHelper;
    private readonly IMapper _mapper;

    public GetColleagueDiscountDetailsQueryHandler(IMongoHelper<Domain.ColleagueDiscount.ColleagueDiscount> colleagueDiscountHelper, IMapper mapper)
    {
        _colleagueDiscountHelper = Guard.Against.Null(colleagueDiscountHelper, nameof(_colleagueDiscountHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<EditColleagueDiscountDto>> Handle(GetColleagueDiscountDetailsQuery request, CancellationToken cancellationToken)
    {
        var colleagueDiscount = await _colleagueDiscountHelper.GetByIdAsync(request.Id);

        if (colleagueDiscount is null)
            throw new NotFoundApiException();

        var mappedColleagueDiscount = _mapper.Map<EditColleagueDiscountDto>(colleagueDiscount);

        return new Response<EditColleagueDiscountDto>(mappedColleagueDiscount);
    }
}