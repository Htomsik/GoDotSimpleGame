namespace SimpleGame.Scripts.Models.Hit.Punch;

public class Punch : Hit<PunchData, PunchBody>
{
    public Punch() : base(new PunchData(), new PunchBody())
    {
    }
}