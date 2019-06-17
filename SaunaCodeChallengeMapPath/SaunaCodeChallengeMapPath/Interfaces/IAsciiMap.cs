using SaunaCodeChallengeMapPath.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaunaCodeChallengeMapPath.Interfaces
{
    public interface IAsciiMap
    {
        IDictionary<IAsciiMapCoordinate, IAsciiField> MapMatrix { get; set; }
        int Rows { get; }
        int Columns { get; }
        IAsciiField StartField { get; }
        List<char> PathCharacters { get; set; }
        List<char> PathLetters { get; set; }

        IAsciiField GetFieldInDirection(PathDirection pathDirection, IAsciiField lastValidField);
        bool IsValid();
        void AddPathCharacter(IAsciiField field);
    }
}
