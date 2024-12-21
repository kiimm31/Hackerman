using Type = StarForcing.Enums.Type;

namespace StarForcing.Models;

public class Equipment
{
    public int Level { get; set; }
    public int StarForce { get; set; }
    public bool Superior { get; set; }
    public Type Type { get; set; }
    public double Cost { get; set; }
}