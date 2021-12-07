namespace DM.Domain.CustomerDiscount;

public class ColleagueDiscount : BaseEntity
{
    #region Properties

    public int Rate { get; set; }

    public bool IsActive { get; set; }

    #endregion

    #region Relations

    public long ProductId { get; set; }

    #endregion
}