using StarForcing.Enums;
using StarForcing.Models;
using static StarForcing.Constants.ConstantRates;

namespace StarForcing.Services;

public class StarForceService(Equipment myItem)
{
    private readonly StarForceDetails _details = new ();
    private int _previousFailCount = 0;

    public void StarForceToNextLevel(bool withStarCatch)
    {
        _details.IncreaseAttempts();
        var myRate = myItem.Superior ? GetSuperiorRates(myItem.StarForce + 1) : GetRates(myItem.StarForce + 1);
        _details.IncreaseCost(CalculateStarForceCost());
        switch (IsChanceTime() ? StarForceResult.Success : myRate.PerformStarForce(withStarCatch))
        {
            case StarForceResult.Success:
                myItem.StarForce++;
                break;
            case StarForceResult.Boom:
                myItem.StarForce = 0;
                _details.IncreaseBoomCount();
                _details.IncreaseCost(myItem.Cost);
                break;
            case StarForceResult.FailAndDrop:
                myItem.StarForce--;
                _previousFailCount++;
                break;
            case StarForceResult.FailAndStay:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    private bool IsChanceTime()
    {
        return _previousFailCount == 2;
    }

    public (Equipment myItem, StarForceDetails details) GetCurrentStatus()
    {
        return (myItem, _details);
    }

    private double CalculateStarForceCost()
    {
        if (myItem.Superior)
        {
            return Math.Round(Math.Pow(myItem.Level, 3.56) / 100) * 100;
        }

        var rLevel = Math.Floor((double)myItem.Level / 10);
        double divisor = 0;
        double power = 2.7;
        double multiplier = 1;

        switch (myItem.StarForce)
        {
            case < 10:
                divisor = 2500;
                power = 1;
                break;
            case < 11:
                divisor = 40000;
                break;
            case < 12:
                divisor = 22000;
                break;
            case < 13:
                divisor = 15000;
                break;
            case < 14:
                divisor = 11000;
                break;
            case < 15:
                divisor = 7500;
                break;
            case < 25:
                divisor = 20000;
                break;
        }

        return Math.Round((
            multiplier * Math.Pow(rLevel, 3) * (Math.Pow(myItem.StarForce + 1, power) / divisor)
        ) + 10) * 100;
    }
}

public class StarForceDetails
{
    private double Cost = 0;
    private int Attempts = 0;
    private int BoomCount = 0;

    public void IncreaseAttempts()
    {
        Attempts++;
    }

    public void IncreaseBoomCount()
    {
        BoomCount++;
    }

    public void IncreaseCost(double cost)
    {
        Cost += cost;
    }

    public override string ToString()
    {
        return $"Attempts: {Attempts}, Boom Count: {BoomCount}, Cost: {Cost:N} mesos";
    }
}