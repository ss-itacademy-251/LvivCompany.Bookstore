using Microsoft.Extensions.Configuration;

namespace LvivCompany.Bookstore.BusinessLogic
{
    public class PathsToDefaultImages
    {
        public readonly string DefaultBookImage;
        public readonly string DefaultProfileImage;
        public PathsToDefaultImages (IConfiguration configuration)
        {
            DefaultBookImage = configuration["DefaultBookImage"];
            DefaultProfileImage = configuration["DefaultUserImage"];
        }
    }
}