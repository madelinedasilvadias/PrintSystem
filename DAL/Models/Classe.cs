﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Classe
    {
        public int ClasseID { get; set; }
        public string Name { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
