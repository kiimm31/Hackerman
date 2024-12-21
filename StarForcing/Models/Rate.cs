using StarForcing.Enums;

namespace StarForcing.Models;

public class Rate(decimal successRate, decimal boomRate, bool drop = false)
{
    private static readonly decimal StarCatchRate = 0.045m * 1000; //starcatch assumption
    private decimal SuccessRate { get; } = successRate * 1000; //0.5 -> convert to base 1000
    private decimal FailRate => 1 - SuccessRate;
    private decimal BoomRate { get; } = boomRate * 1000;
    private bool IsDropStar { get; } = drop;

    public StarForceResult PerformStarForce(bool withStarCatch)
    {
        var rng = new Random();
        var random = rng.Next(1, 1001); // returns 1 - 1000

        var mySuccessRate = withStarCatch ? SuccessRate + StarCatchRate : SuccessRate;

        if (random <= mySuccessRate)
        {
            return StarForceResult.Success;
        }

        if (BoomRate > 0)
        {
            var boom = rng.Next(1, 1001);
            if (boom <= BoomRate)
            {
                return StarForceResult.Boom;
            }
        }

        return IsDropStar ? StarForceResult.FailAndDrop : StarForceResult.FailAndStay;
    }
}