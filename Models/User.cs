using SQLite;

namespace HerbShop.Models
{
    public class User
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
