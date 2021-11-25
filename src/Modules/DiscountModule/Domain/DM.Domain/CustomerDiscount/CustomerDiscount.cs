namespace DM.Domain.CustomerDiscount;

public class CustomerDiscount : BaseEntity
{
    #region Properties

    public int Rate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Description { get; set; }

    #endregion

    #region Relations

    public long ProductId { get; set; }

    #endregion
}