namespace galaxy_match_make.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public required string OAuthId { get; set; }
        public bool Inactive { get; set; }

        public ProfileDto? Profile { get; set; }

    }
}
