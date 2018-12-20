using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_9_Standalone.Models
{
    public class RegisterUserModel
    {
        private static Faker<RegisterUserModel> rules; 
        public RegisterUserModel()
        {
            rules = new Faker<RegisterUserModel>().
                RuleFor(u => u.Email, f => f.Random.Replace("*****@gmail.com")).
                RuleFor(u => u.Name, f => f.Random.Word()).
                RuleFor(u => u.Surname, f => f.Random.Word()).
                RuleFor(u => u.Company, f => f.Random.Word()).
                RuleFor(u => u.Password, f => f.Random.Replace("???##_****a"));
        }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public string Password { get; set; }

        public RegisterUserModel GenerateFakeUser()
        {
            return rules.Generate();
        }
    }
}
