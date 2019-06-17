using SaunaCodeChallengeMapPath.Helpers;
using SaunaCodeChallengeMapPath.Interfaces;
using SaunaCodeChallengeMapPath.Shared;
using System.Collections.Generic;
using System.Linq;

namespace SaunaCodeChallengeMapPath.Models
{
    public class AsciiMap : IAsciiMap
    {
        public IDictionary<IAsciiMapCoordinate, IAsciiField> MapMatrix { get; set; }
        public int Rows { get { return MapMatrix.Keys.Select(x => x.Row).Max(); } }
        public int Columns { get { return MapMatrix.Keys.Select(x => x.Column).Max(); } }
        public IAsciiField StartField
        {
            get
            {
                return MapMatrix.Values
                    .Where(x => Pathfinder.IsAsciiStartCharacter(x.Character))
                    .FirstOrDefault();
            }
        }
        public List<char> PathCharacters { get; set; }
        public List<char> PathLetters { get; set; }

        public AsciiMap()
        {
            PathCharacters = new List<char>();
            PathLetters = new List<char>();
        }

        public IAsciiField GetFieldInDirection(PathDirection pathDirection, IAsciiField lastValidField)
        {
            IAsciiField field = null;

            switch (pathDirection)
            {
                case PathDirection.East:
                    field = GetField(lastValidField.Row, lastValidField.Column + 1);
                    break;
                case PathDirection.North:
                    field = GetField(lastValidField.Row - 1, lastValidField.Column);
                    break;
                case PathDirection.South:
                    field = GetField(lastValidField.Row + 1, lastValidField.Column);
                    break;
                case PathDirection.West:
                    field = GetField(lastValidField.Row, lastValidField.Column - 1);
                    break;
                default:
                    break;
            }

            return field;
        }

        public bool IsValid()
        {
            return MapMatrix.Count > 0;
        }

        public void AddPathCharacter(IAsciiField field)
        {
            PathCharacters.Add(field.Character);

            if (!field.AlreadyVisited
                && field.Character >= (int)MapCharacter.FirstTextCharacter
                && field.Character <= (int)MapCharacter.LastTextCharacter)
            {
                PathLetters.Add(field.Character);
            }

            field.AlreadyVisited = true;
        }

        private IAsciiField GetField(int row, int column)
        {
            return MapMatrix
                .Where(x => x.Key.Row == row && x.Key.Column == column)
                .Select(y => y.Value)
                .FirstOrDefault();
        }
    }
}
