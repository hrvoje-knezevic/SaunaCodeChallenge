using SaunaCodeChallengeMapPath.Interfaces;
using SaunaCodeChallengeMapPath.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SaunaCodeChallengeMapPath.Helpers
{
    public class Pathfinder
    {
        #region Public interface

        public static void FindPath(IAsciiMap map, IAsciiField currentField, PathDirection currentDirection)
        {
            map.AddPathCharacter(currentField);
            var directionToMoveNext = ScanArea(map, currentField, currentDirection);
            IAsciiField nextField = map.GetFieldInDirection(directionToMoveNext, currentField);
            if (IsAsciiEndCharacter(nextField.Character))
            {
                map.AddPathCharacter(nextField);
            }
            else
            {
                FindPath(map, nextField, directionToMoveNext);
            }
        }

        public static PathDirection ScanArea(IAsciiMap map, IAsciiField currentField, PathDirection currentPathDirection)
        {
            if (currentPathDirection != PathDirection.Unknown
                && IsDirectionPreserved(map, currentField, currentPathDirection))
            {
                return currentPathDirection;
            }

            foreach (PathDirection direction in Enum.GetValues(typeof(PathDirection)))
            {
                if (IsDirectionValid(map, currentField, direction))
                {
                    return direction;
                }
            }

            return PathDirection.Unknown;
        }

        public static bool IsAsciiStartCharacter(char character)
        {
            return character == (int)MapCharacter.StartPosition;
        }

        public static bool IsAsciiEndCharacter(char character)
        {
            return character == (int)MapCharacter.EndPosition;
        }

        public static bool IsPassableCharacter(char character)
        {
            List<int> mapManipulationCharacters = Enum.GetValues(typeof(MapCharacter)).Cast<int>().ToList();
            mapManipulationCharacters.AddRange(Enumerable.Range((int)MapCharacter.FirstTextCharacter + 1, (int)MapCharacter.LastTextCharacter - (int)MapCharacter.FirstTextCharacter - 1));

            return mapManipulationCharacters.Contains(character);
        }

        #endregion


        #region Private implementation

        private static bool IsDirectionValid(IAsciiMap map, IAsciiField field, PathDirection direction)
        {
            var nextField = map.GetFieldInDirection(direction, field);

            return nextField != null
                && !nextField.AlreadyVisited
                && IsPassableCharacter(nextField.Character);
        }

        private static bool IsDirectionPreserved(IAsciiMap map, IAsciiField field, PathDirection direction)
        {
            var nextField = map.GetFieldInDirection(direction, field);

            return nextField != null
                && IsPassableCharacter(nextField.Character);
        }

        #endregion
    }
}
