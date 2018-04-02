using FileGenerator.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileGenerator.Models
{
    public class ClientModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public GenderEnum Gender { get; set; }

        public DateTime Date { get; set; }

        public int Points { get; set; }
    }
}