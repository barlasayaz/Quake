using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quake.Dto
{ 
    public class ApiUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
