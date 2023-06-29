namespace SimpleGame.Scripts.Models.Hit.Punch;

/// <summary>
///     Удар рукой
/// </summary>
public class Punch : Hit<PunchData, PunchBody>
{
    public Punch() : base(new PunchData(), new PunchBody())
    {
    }
}