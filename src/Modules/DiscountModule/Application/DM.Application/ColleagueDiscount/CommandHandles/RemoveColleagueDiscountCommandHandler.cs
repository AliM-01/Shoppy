using DM.Application.Contracts.ColleagueDiscount.Commands;

namespace DM.Application.ColleagueDiscount.CommandHandles;

public class RemoveColleagueDiscountCommandHandler : IRequestHandler<RemoveColleagueDiscountCommand, Response<string>>
{
    #region Ctor

    private readonly IMongoHelper<Domain.ColleagueDiscount.ColleagueDiscount> _colleagueDiscountHelper;

    public RemoveColleagueDiscountCommandHandler(IMongoHelper<Domain.ColleagueDiscount.ColleagueDiscount> colleagueDiscountHelper)
    {
        _colleagueDiscountHelper = Guard.Against.Null(colleagueDiscountHelper, nameof(_colleagueDiscountHelper));
    }

    #endregion

    public async Task<Response<string>> Handle(RemoveColleagueDiscountCommand request, CancellationToken cancellationToken)
    {
        var colleagueDiscount = await _colleagueDiscountHelper.GetByIdAsync(request.ColleagueDiscountId);

        if (colleagueDiscount is null)
            throw new NotFoundApiException();

        colleagueDiscount.IsActive = false;

        await _colleagueDiscountHelper.UpdateAsync(colleagueDiscount);

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}