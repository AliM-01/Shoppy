using _0_Framework.Application.Exceptions;
using _0_Framework.Application.Utilities.ImageRelated;
using AM.Application.Account.DTOs;

namespace AM.Application.Account.Commands;

public record EditAccountCommand(EditAccountDto Account) : IRequest<ApiResult>;

public class EditAccountCommandHandler : IRequestHandler<EditAccountCommand, ApiResult>
{
    private readonly IMapper _mapper;
    private readonly UserManager<Domain.Account.Account> _userManager;

    public EditAccountCommandHandler(IMapper mapper,
                                         UserManager<Domain.Account.Account> userManager)
    {
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
    }

    public async Task<ApiResult> Handle(EditAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Account.Id);

        NotFoundApiException.ThrowIfNull(user);

        _mapper.Map(request.Account, user);

        if (request.Account.ImageFile is not null)
        {
            string imagePath = request.Account.ImageFile.GenerateImagePath();

            request.Account.ImageFile.CropAndAddImageToServer(imagePath, PathExtension.Avatar200, 200, 200);
            request.Account.ImageFile.CropAndAddImageToServer(imagePath, PathExtension.Avatar60, 60, 60);

            user.Avatar = imagePath;
        }

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
            throw new ApiException($"${result.Errors.First().Description}");

        return ApiResponse.Success("کاربر با موفقیت ویرایش شد");
    }
}