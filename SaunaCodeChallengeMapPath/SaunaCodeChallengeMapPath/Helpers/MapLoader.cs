using SaunaCodeChallengeMapPath.Interfaces;
using SaunaCodeChallengeMapPath.Models;
using SaunaCodeChallengeMapPath.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SaunaCodeChallengeMapPath.Helpers
{
    public class MapLoader
    {
        #region Public interface

        public static IAsciiMap LoadAsciiMap(MapSelector mapsSelector)
        {
            IAsciiMap map = new AsciiMap();
            using (var mapResource = LoadMapFromEmbeddedResource(mapsSelector))
            {
                map.MapMatrix = GetMapMatrixFromResource(mapResource);
            }

            return map;
        }

        public static Stream LoadMapFromEmbeddedResource(MapSelector mapsSelector)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var mapName = GetMapName(assembly, mapsSelector);

            if (mapName == null)
            {
                return null;
            }

            return assembly.GetManifestResourceStream(mapName);
        }

        public static IDictionary<IAsciiMapCoordinate, IAsciiField> GetMapMatrixFromResource(Stream stream)
        {
            var mapMatrix = new Dictionary<IAsciiMapCoordinate, IAsciiField>();

            if (stream == null)
            {
                return mapMatrix;
            }

            using (var mapReader = new StreamReader(stream))
            {
                string mapRow;
                int i = 1;
                while ((mapRow = mapReader.ReadLine()) != null)
                {
                    int j = 1;
                    foreach (var character in mapRow.ToCharArray())
                    {
                        mapMatrix.Add(new AsciiMapCoordinate(i, j), new AsciiField(i, j, character));
                        j++;
                    }
                    i++;
                }
            }

            return mapMatrix;
        }

        #endregion


        #region Private implementation

        private static string GetMapName(Assembly assembly, MapSelector mapsSelector)
        {
            var resourceNames = assembly.GetManifestResourceNames();

            return Array.Find(resourceNames, x => x.Contains(mapsSelector.ToString()));
        }

        #endregion
    }
}
