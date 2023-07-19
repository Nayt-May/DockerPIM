using System.Security.Claims;
using System.Text.Json.Serialization;

namespace LincolnAPI.Identity
{
    public class TokenGenerationRequest
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public String Email { get; set; }
        public Dictionary<String,String> Roles { get; set; }
    }
}