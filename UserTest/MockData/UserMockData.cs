using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCrud.Models;

namespace UserTest.MockData
{
    public class UserMockData
    {
        public static List<User> GetUsers()
        {
            return new List<User>{
             new User{
                 UserId = 1,
                 NamaLengkap = "Arief",
                 Username = "a",
                 Password = "b",
                 Status ="Y"
             },
             new User{
                 UserId = 2,
                 NamaLengkap = "Arief",
                 Username = "a",
                 Password = "b",
                 Status ="Y"
             },
             new User{
                 UserId = 2,
                 NamaLengkap = "Arief",
                 Username = "a",
                 Password = "b",
                 Status ="Y"
             }
         };
        }
        public static List<User> GetEmptyUsers()
        {
            return new List<User>();
        }
        public static User NewUser()
        {
            return new User
            {
                UserId = 0,
                NamaLengkap = "Arief",
                Username = "a",
                Password = "b",
                Status = "Y"
            };
        }
    }
}
