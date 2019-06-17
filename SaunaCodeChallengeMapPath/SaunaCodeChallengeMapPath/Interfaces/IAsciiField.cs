namespace SaunaCodeChallengeMapPath.Interfaces
{
    public interface IAsciiField
    {
        int Row { get; set; }
        int Column { get; set; }
        char Character { get; set; }
        bool AlreadyVisited { get; set; }
    }
}
