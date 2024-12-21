// See https://aka.ms/new-console-template for more information

using StarForcing.Models;
using StarForcing.Services;
using Type = StarForcing.Enums.Type;

Console.WriteLine("Hello, World!");

Console.WriteLine("Current Star:");
int currentStar = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Target Star:");
int target = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("With Star Catch:");
bool withStarCatch = Convert.ToBoolean(Console.ReadLine());

var mySuperiorItem = new Equipment()
{
    Level = 150,
    StarForce = currentStar,
    Superior = true,
    Type = Type.Belt
};

var sfService = new StarForceService(mySuperiorItem);

while (mySuperiorItem.StarForce < target)
{
    sfService.StarForceToNextLevel(withStarCatch);
}

var (myItem, cost) = sfService.GetCurrentStatus();

Console.WriteLine($"Current Star: {myItem.StarForce}");
Console.WriteLine($"Cost: {cost}");

