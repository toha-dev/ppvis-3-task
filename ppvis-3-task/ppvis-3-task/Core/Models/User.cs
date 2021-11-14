using Core.Utils;

namespace Core.Models
{
    public class User : SingletonFactory<User>
    {
        public string Id { get; private set; }
        public Disease CurrentDisease { get; set; }

        public User(Disease currentDisease, string id)
        {
            Id = id;
            CurrentDisease = currentDisease;
        }
    }
}
