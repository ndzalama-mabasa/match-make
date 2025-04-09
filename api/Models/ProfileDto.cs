namespace galaxy_match_make.Models
{
    public class ProfileDto
    {
        public int Id { get; set; }
        public required Guid UserId { get; set; }
        public required string DisplayName { get; set; }
        public string? Bio { get; set; }
        public string? AvatarUrl { get; set; }
        public required int SpeciesId { get; set; }
        public required int PlanetId { get; set; }
        public int? GenderId { get; set; }
        public float? HeightInGalacticInches { get; set; }
        public int? GalacticDateOfBirth { get; set; }
    }
}
