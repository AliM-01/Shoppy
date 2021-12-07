using DM.Application.Contracts.ColleagueDiscount.Commands;

namespace DM.Application.ColleagueDiscount.CommandHandles;

public class RestoreColleagueDiscountCommandHandler : IRequestHandler<RestoreColleagueDiscountCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ColleagueDiscount.ColleagueDiscount> _colleagueDiscountRepository;

    public RestoreColleagueDiscountCommandHandler(IGenericRepository<Domain.ColleagueDiscount.ColleagueDiscount> colleagueDiscountRepository)
    {
        _colleagueDiscountRepository = Guard.Against.Null(colleagueDiscountRepository, nameof(_colleagueDiscountRepository));
    }

    #endregion

    public async Task<Response<string>> Handle(RestoreColleagueDiscountCommand request, CancellationToken cancellationToken)
    {
        var colleagueDiscount = await _colleagueDiscountRepository.GetEntityById(request.ColleagueDiscountId);

        if (colleagueDiscount is null)
            throw new NotFoundApiException();

        colleagueDiscount.IsActive = true;

        _colleagueDiscountRepository.Update(colleagueDiscount);
        await _colleagueDiscountRepository.SaveChanges();

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}