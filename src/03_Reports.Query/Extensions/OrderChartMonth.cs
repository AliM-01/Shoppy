namespace _03_Reports.Query.Extensions;

public static class OrderChartMonth
{
    public static List<ChartModel> OrderMonth(this List<ChartModel> value)
    {
        foreach (var item in value)
        {
            switch (item.Month)
            {
                case 1:
                    item.MonthOrder = 10;
                    break;

                case 2:
                    item.MonthOrder = 11;
                    break;

                case 3:
                    item.MonthOrder = 12;
                    break;

                case 4:
                    item.MonthOrder = 1;
                    break;

                case 5:
                    item.MonthOrder = 2;
                    break;

                case 6:
                    item.MonthOrder = 3;
                    break;
                case 7:
                    item.MonthOrder = 4;
                    break;
                case 8:
                    item.MonthOrder = 5;
                    break;
                case 9:
                    item.MonthOrder = 6;
                    break;
                case 10:
                    item.MonthOrder = 7;
                    break;
                case 11:
                    item.MonthOrder = 8;
                    break;
                case 12:
                    item.MonthOrder = 9;
                    break;
            }
        }

        return value.OrderBy(x => x.MonthOrder).ToList();
    }
}