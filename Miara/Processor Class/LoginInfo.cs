using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Net.NetworkInformation;

namespace Miara.Models
{
    [Serializable]
    public class LoginInfo
    {
        public string DataSource { get; set; }
        public string SelectedDatabase { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class EmployeeDetails
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
    }
}