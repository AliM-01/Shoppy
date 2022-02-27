using _0_Framework.Application.Exceptions;
using _0_Framework.Application.Extensions;
using _0_Framework.Application.Utilities.ImageRelated;
namespace AM.Application.Account.CommandHandles;

public class EditAccountCommandHandler : IRequestHandler<EditAccountCommand, Response<string>>
{
    #region Ctor

    private readonly IMapper _mapper;
    private readonly UserManager<Domain.Account.Account> _userManager;

    public EditAccountCommandHandler(IMapper mapper,
                                         UserManager<Domain.Account.Account> userManager)
    {
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
    }

    #endregion Ctor

    public async Task<Response<string>> Handle(EditAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Account.Id);

        if (user is null)
            throw new NotFoundApiException();

        _mapper.Map(request.Account, user);

        if (request.Account.ImageFile is not null)
        {
            var imagePath = DateTime.Now.ToFileName() + Path.GetExtension(request.Account.ImageFile.FileName);

            request.Account.ImageFile.CropAndAddImageToServer(imagePath, PathExtension.AccountImage, 150, 150);

            user.Avatar = imagePath;
        }

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
            throw new ApiException($"${result.Errors.First()}");

        return new Response<string>("کاربر با موفقیت ویرایش شد");
    }
}
