using FileGenerator.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileGenerator.Models
{
    public class FileGeneratorModel
    {
        public string Name { get; set; }

        public FileTypeEnum FileType { get; set; }
    }
}