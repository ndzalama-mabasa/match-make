namespace galaxy_match_make.Models
{
    public class ProfileDto
    {
        public int Id { get; set; }
        public required Guid User_Id { get; set; }
        public required string DisplayName { get; set; }
        public string? Bio { get; set; }
        public string? AvatarUrl { get; set; }

        public float? HeightInGalacticInches { get; set; }
        public int? GalacticDateOfBirth { get; set; }

        public required UserDto User { get; set; }
        public SpeciesDto? Species { get; set; }
        public PlanetDto? Planet { get; set; }
        public GenderDto? Gender { get; set; }

    }
}
