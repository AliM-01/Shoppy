using _0_Framework.Infrastructure;

namespace DM.Infrastructure.Persistence.Settings;

public class DiscountDbSettings : BaseMongoDbSettings
{
    public string ColleagueDiscountCollection { get; set; }

    public string ProductDiscountCollection { get; set; }
}
