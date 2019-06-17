using System.ComponentModel.DataAnnotations;

namespace SaunaCodeChallengeMapPath.Shared
{
    public enum MapSelector
    {
        Map1,
        Map2,
        Map3,
        Map4
    }

    public enum PathDirection
    {
        Unknown,
        East,
        North,
        South,
        West
    }

    public enum MapCharacter
    {
        [Display(Name = "@")]
        StartPosition = 64,
        [Display(Name = "|")]
        VerticalDirection = 124,
        [Display(Name = "-")]
        HorizontalDirection = 45,
        [Display(Name = "+")]
        ChangeDirection = 43,
        [Display(Name = "x")]
        EndPosition = 120,
        [Display(Name = "A")]
        FirstTextCharacter = 65,
        [Display(Name = "Z")]
        LastTextCharacter = 90
    }
}
