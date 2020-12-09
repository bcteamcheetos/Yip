namespace YipRestaurantApp.Models
{
    public class UserModel    ////Log in
    {
        public string FirstName { get; set; }//Setting property for the username
        public string Password { get; set; }//setting property for the property
        public UserModel() { }
        public UserModel(string name, string password)
        {
            FirstName = name;
            Password = password;
        }
    }
}
