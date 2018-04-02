using FileGenerator.Enum;
using FileGenerator.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileGenerator.Models
{
    public class TXTFile : IFile
    {
        public FileTypeEnum Type
        {
            get { return FileTypeEnum.TXT; }
            set { Type = value; }
        }
        public string ContentType { get { return string.Empty; } }
        public string ContentDisposition { get { return string.Empty; } }
        public string ConvertFile(string json) => throw new NotImplementedException();
        public string StrFile { get; set; }

    }
}