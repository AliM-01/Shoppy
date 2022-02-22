using DM.Application.Contracts.ColleagueDiscount.Commands;

namespace DM.Application.ColleagueDiscount.CommandHandles;

public class DeleteColleagueDiscountCommandHandler : IRequestHandler<DeleteColleagueDiscountCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ColleagueDiscount.ColleagueDiscount> _colleagueDiscountRepository;

    public DeleteColleagueDiscountCommandHandler(IGenericRepository<Domain.ColleagueDiscount.ColleagueDiscount> colleagueDiscountRepository)
    {
        _colleagueDiscountRepository = Guard.Against.Null(colleagueDiscountRepository, nameof(_colleagueDiscountRepository));
    }

    #endregion

    public async Task<Response<string>> Handle(DeleteColleagueDiscountCommand request, CancellationToken cancellationToken)
    {
        var colleagueDiscount = await _colleagueDiscountRepository.GetByIdAsync(request.ColleagueDiscountId);

        if (colleagueDiscount is null)
            throw new NotFoundApiException();

        await _colleagueDiscountRepository.DeletePermanentAsync(colleagueDiscount.Id);

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}