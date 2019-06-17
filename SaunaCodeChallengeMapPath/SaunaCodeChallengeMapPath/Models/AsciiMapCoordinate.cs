using SaunaCodeChallengeMapPath.Interfaces;

namespace SaunaCodeChallengeMapPath.Models
{
    public class AsciiMapCoordinate : IAsciiMapCoordinate
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public AsciiMapCoordinate()
        {
        }

        public AsciiMapCoordinate(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
