using DM.Application.Contracts.ColleagueDiscount.Commands;

namespace DM.Application.ColleagueDiscount.CommandHandles;

public class DeleteColleagueDiscountCommandHandler : IRequestHandler<DeleteColleagueDiscountCommand, Response<string>>
{
    #region Ctor

    private readonly IMongoHelper<Domain.ColleagueDiscount.ColleagueDiscount> _colleagueDiscountHelper;

    public DeleteColleagueDiscountCommandHandler(IMongoHelper<Domain.ColleagueDiscount.ColleagueDiscount> colleagueDiscountHelper)
    {
        _colleagueDiscountHelper = Guard.Against.Null(colleagueDiscountHelper, nameof(_colleagueDiscountHelper));
    }

    #endregion

    public async Task<Response<string>> Handle(DeleteColleagueDiscountCommand request, CancellationToken cancellationToken)
    {
        var colleagueDiscount = await _colleagueDiscountHelper.GetByIdAsync(request.ColleagueDiscountId);

        if (colleagueDiscount is null)
            throw new NotFoundApiException();

        await _colleagueDiscountHelper.DeletePermanentAsync(colleagueDiscount.Id);

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}