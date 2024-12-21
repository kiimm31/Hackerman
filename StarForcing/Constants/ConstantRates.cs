using StarForcing.Models;

namespace StarForcing.Constants;

public static class ConstantRates
{
    private static readonly Dictionary<int, Rate> SuperiorStarForceRate = new()
    {
        { 1, new Rate(0.5m, 0, false) },
        { 2, new Rate(0.5m, 0, true) },
        { 3, new Rate(0.45m, 0, true) },
        { 4, new Rate(0.4m, 0, true) },
        { 5, new Rate(0.4m, 0, true) },
        { 6, new Rate(0.4m, 0.018m, true) },
        { 7, new Rate(0.4m, 0.03m, true) },
        { 8, new Rate(0.4m, 0.042m, true) },
        { 9, new Rate(0.4m, 0.06m, true) },
        { 10, new Rate(0.37m, 0.0945m, true) },
        { 11, new Rate(0.35m, 0.13m, true) },
        { 12, new Rate(0.35m, 0.1625m, true) },
        { 13, new Rate(0.03m, 0.485m, true) },
        { 14, new Rate(0.02m, 0.49m, true) },
        { 15, new Rate(0.01m, 0.49m, true) }
    };

    public static Rate GetSuperiorRates(int star)
    {
        if (SuperiorStarForceRate.TryGetValue(star, out var myRate))
        {
            return myRate;
        }

        throw new Exception("Star not found");
    }

    public static Rate GetRates(int star)
    {
        double base_success_rate = 1;
        double success_rate = 0;
        double sdiff = 0;

        switch (star)
        {
            case < 15:
            {
                sdiff = (star + 1);

                if (star > 2) sdiff -= 1;

                sdiff = sdiff * 0.05;
                break;
            }
            case < 22:
                sdiff = 0.7;
                break;
            case < 25:
                sdiff = (75 + star) / 100;
                break;
        }

        var destroy_rate = star switch
        {
            < 15 => 0,
            < 18 => 0.021,
            < 20 => 0.028,
            < 22 => 0.07,
            22 => 0.194,
            23 => 0.294,
            24 => 0.396,
            _ => 0
        };
        
        var willNotDropRank = star is <= 15 or 20 or 25;

        success_rate = Math.Round(base_success_rate - sdiff, 2);

        return new Rate((decimal)success_rate / 1000, (decimal)destroy_rate / 1000, !willNotDropRank);
    }
}