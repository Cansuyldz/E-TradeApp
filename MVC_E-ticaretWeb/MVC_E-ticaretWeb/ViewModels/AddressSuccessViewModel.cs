using MVC_E_ticaretWeb.Models;

namespace MVC_E_ticaretWeb.ViewModels
{
    public class AddressSuccessViewModel
    {
       public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Streetaddress { get; set; }
        public string AddressLine { get; set; } 
        public string Province { get; set; } 
        public string District { get; set; }
        public string Neighbourhood { get; set; }
        public bool Success { get; set; } = false;
        public string Message { get; set; }
    }
}
