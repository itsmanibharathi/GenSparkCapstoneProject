using api.Models;

namespace apiUnitTest
{
    public static class SeedData
    {
        public static List<User> Users = new List<User>
        {
            new User
            {
                UserName = "user1",
                UserEmail= "user1@email.com",
                UserPhoneNumber = "1234567890",
            },
            new User
            {
                UserName = "user2",
                UserEmail= "user2@email.com",
                UserPhoneNumber = "1234567890",
            }
        };
    }
}