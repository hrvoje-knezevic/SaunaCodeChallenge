using log4net;
using SaunaCodeChallengeMapPath.Helpers;
using SaunaCodeChallengeMapPath.Interfaces;
using SaunaCodeChallengeMapPath.Shared;
using System;
using System.Reflection;

namespace SaunaCodeChallengeMapPath
{
    class AsciiMapCodeChallenge
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            log.Info("Ascii map pathfinder started!");

            IAsciiMap map;
            foreach (MapSelector mapName in Enum.GetValues(typeof(MapSelector)))
            {
                map = LoadAsciiMap(mapName);

                if (!map.IsValid())
                {
                    log.Warn($"{mapName} is not properly loaded!");
                    continue;
                }

                log.Info($"{mapName} loaded! Searching for path...");

                try
                {
                    Pathfinder.FindPath(map, map.StartField, PathDirection.Unknown);
                    log.Info($"Characters: {string.Join("", map.PathCharacters.ToArray())}");
                    log.Info($"Letters: {string.Join("", map.PathLetters.ToArray())}");
                }
                catch (Exception e)
                {
                    log.Error("Error while finding ascii map path!", e);
                    Environment.Exit(1);
                }
            }
        }

        private static IAsciiMap LoadAsciiMap(MapSelector mapSelector)
        {
            IAsciiMap map = null;
            try
            {
                map = MapLoader.LoadAsciiMap(mapSelector);
            }
            catch (Exception e)
            {
                log.Error("Error loading ascii map!", e);
                Environment.Exit(1);
            }

            return map;
        }
    }
}
