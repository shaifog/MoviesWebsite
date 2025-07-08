namespace EX.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; } = true;

        public static List<User> usersList = new List<User>();

        public static bool Insert(User u)
        {
            if (usersList.Any(x => x.Id == u.Id))
                return false;

            usersList.Add(u);
            return true;
        }

        public static List<User> Read()
        {
            return usersList;
        }
    }
}
