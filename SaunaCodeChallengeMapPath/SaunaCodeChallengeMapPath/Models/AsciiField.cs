using SaunaCodeChallengeMapPath.Interfaces;

namespace SaunaCodeChallengeMapPath.Models
{
    public class AsciiField : IAsciiField
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public char Character { get; set; }
        public bool AlreadyVisited { get; set; }

        public AsciiField(int row, int column, char character)
        {
            Row = row;
            Column = column;
            Character = character;
            AlreadyVisited = false;
        }

    }
}
