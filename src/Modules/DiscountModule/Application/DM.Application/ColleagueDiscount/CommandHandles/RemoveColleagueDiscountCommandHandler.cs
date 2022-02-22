using DM.Application.Contracts.ColleagueDiscount.Commands;

namespace DM.Application.ColleagueDiscount.CommandHandles;

public class RemoveColleagueDiscountCommandHandler : IRequestHandler<RemoveColleagueDiscountCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ColleagueDiscount.ColleagueDiscount> _colleagueDiscountRepository;

    public RemoveColleagueDiscountCommandHandler(IGenericRepository<Domain.ColleagueDiscount.ColleagueDiscount> colleagueDiscountRepository)
    {
        _colleagueDiscountRepository = Guard.Against.Null(colleagueDiscountRepository, nameof(_colleagueDiscountRepository));
    }

    #endregion

    public async Task<Response<string>> Handle(RemoveColleagueDiscountCommand request, CancellationToken cancellationToken)
    {
        var colleagueDiscount = await _colleagueDiscountRepository.GetByIdAsync(request.ColleagueDiscountId);

        if (colleagueDiscount is null)
            throw new NotFoundApiException();

        colleagueDiscount.IsActive = false;

        await _colleagueDiscountRepository.UpdateAsync(colleagueDiscount);

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}