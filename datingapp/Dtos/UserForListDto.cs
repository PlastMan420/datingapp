﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace datingapp.Dtos
{   // a view of a list of users
    public class UserForListDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string gender { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhotoUrl { get; set; }
    }
}
