using SaunaCodeChallengeMapPath.Helpers;
using SaunaCodeChallengeMapPath.Interfaces;
using SaunaCodeChallengeMapPath.Models;
using SaunaCodeChallengeMapPath.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace SaunaCodeChallengeTest
{
    public class AsciiManipulationTest
    {
        [Theory]
        [InlineData(MapSelector.Map1, 45)]
        [InlineData(MapSelector.Map2, 70)]
        public void GetMapMatrixFromResource_AciiMapFromResource_ReturnsMap(
            MapSelector mapsSelector,
            int expectedMapMatrixFieldCount)
        {
            IDictionary<IAsciiMapCoordinate, IAsciiField> map;

            using (var mapResource = TestResource.GetResourceMap(mapsSelector))
            {
                map = MapLoader.GetMapMatrixFromResource(mapResource);
            }

            Assert.Equal(map.Count, expectedMapMatrixFieldCount);
        }

        [Theory]
        [InlineData(MapSelector.Map1, 53)]
        [InlineData(MapSelector.Map2, 82)]
        [InlineData(MapSelector.Map3, 106)]
        public void LoadMapFromEmbeddedResource_ReadMapFile_ReturnsMapCharacters(MapSelector mapsSelector, int mapCharacterLengthExpected)
        {
            int mapCharactersLength = 0;

            using (var mapResource = MapLoader.LoadMapFromEmbeddedResource(mapsSelector))
            {
                StreamReader mapReader = new StreamReader(mapResource);
                mapCharactersLength = mapReader.ReadToEnd().Length;
            }

            Assert.Equal(mapCharacterLengthExpected, mapCharactersLength);
        }

        [Fact]
        public void LoadAsciiMap_LoadsGivenMap_ReturnsMap()
        {
            var map = MapLoader.LoadAsciiMap(MapSelector.Map2);

            Assert.Equal(7, map.Rows);
            Assert.Equal(10, map.Columns);
        }

        [Fact]
        public void FindPath_GetMapPathCharacters_ReturnUpdatedPathCharacters()
        {
            IAsciiMap map = new AsciiMap();
            string expectedPathCharacters = "@---+B||E--+|E|+--F--+|C|||A--|-----K|||+--E--Ex";

            using (var mapResource = TestResource.GetResourceMap(MapSelector.Map3))
            {
                map.MapMatrix = MapLoader.GetMapMatrixFromResource(mapResource);
            }
            Pathfinder.FindPath(map, map.StartField, PathDirection.Unknown);

            Assert.Equal(expectedPathCharacters, string.Join("", map.PathCharacters.ToArray()));
        }

        [Fact]
        public void FindPath_GetMapPathLetters_ReturnUpdatedPathLetters()
        {
            IAsciiMap map = new AsciiMap();
            string expectedPathLetters = "SAUNA";

            using (var mapResource = TestResource.GetResourceMap(MapSelector.Map4))
            {
                map.MapMatrix = MapLoader.GetMapMatrixFromResource(mapResource);
            }
            Pathfinder.FindPath(map, map.StartField, PathDirection.Unknown);

            Assert.Equal(expectedPathLetters, string.Join("", map.PathLetters.ToArray()));
        }

        [Fact]
        public void ScanArea_SearchForPathDirection_ReturnNextDirection()
        {
            var map = MapLoader.LoadAsciiMap(MapSelector.Map3);
            var startField = map.StartField;
            var currentDirection = PathDirection.Unknown;
            var expectedDirection = PathDirection.East;

            var direction = Pathfinder.ScanArea(map, startField, currentDirection);

            Assert.Equal(expectedDirection, direction);
        }

        [Theory]
        [InlineData('A', true)]
        [InlineData('?', false)]
        [InlineData('+', true)]
        [InlineData('j', false)]
        public void IsPassableCharacter_CheckIfCharacterIsPartOfPath_ReturnPathPartConfirmation(char character, bool expectedPassability)
        {
            bool isPassable = Pathfinder.IsPassableCharacter(character);

            Assert.Equal(expectedPassability, isPassable);
        }

        [Theory]
        [InlineData((int)MapCharacter.StartPosition, true)]
        [InlineData((int)MapCharacter.EndPosition, false)]
        public void IsAsciiStartCharacter_ChecksForStartCharacter_ReturnTrueIfStartCharacter(char character, bool expected)
        {
            bool isTrue = Pathfinder.IsAsciiStartCharacter(character);

            Assert.Equal(expected, isTrue);
        }

        [Theory]
        [InlineData((int)MapCharacter.StartPosition, false)]
        [InlineData((int)MapCharacter.EndPosition, true)]
        public void IsAsciiEndCharacter_ChecksForEndCharacter_ReturnTrueIfEndCharacter(char character, bool expected)
        {
            bool isTrue = Pathfinder.IsAsciiEndCharacter(character);

            Assert.Equal(expected, isTrue);
        }
    }
}
