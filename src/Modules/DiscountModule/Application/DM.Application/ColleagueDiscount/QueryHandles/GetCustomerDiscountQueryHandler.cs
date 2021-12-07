using AutoMapper;
using DM.Application.Contracts.ColleagueDiscount.DTOs;
using DM.Application.Contracts.ColleagueDiscount.Queries;

namespace DM.Application.ColleagueDiscount.QueryHandles;
public class GetColleagueDiscountQueryHandler : IRequestHandler<GetColleagueDiscountDetailsQuery, Response<EditColleagueDiscountDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ColleagueDiscount.ColleagueDiscount> _colleagueDiscountRepository;
    private readonly IMapper _mapper;

    public GetColleagueDiscountQueryHandler(IGenericRepository<Domain.ColleagueDiscount.ColleagueDiscount> colleagueDiscountRepository, IMapper mapper)
    {
        _colleagueDiscountRepository = Guard.Against.Null(colleagueDiscountRepository, nameof(_colleagueDiscountRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<EditColleagueDiscountDto>> Handle(GetColleagueDiscountDetailsQuery request, CancellationToken cancellationToken)
    {
        var colleagueDiscount = await _colleagueDiscountRepository.GetEntityById(request.Id);

        if (colleagueDiscount is null)
            throw new NotFoundApiException();

        var mappedColleagueDiscount = _mapper.Map<EditColleagueDiscountDto>(colleagueDiscount);

        return new Response<EditColleagueDiscountDto>(mappedColleagueDiscount);
    }
}