using FileGenerator.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileGenerator.Interface
{
    public interface IFile
    {
        FileTypeEnum Type { get; set; }
        string ContentType { get; }
        string ContentDisposition { get; }
        string ConvertFile(string json);
        string StrFile { get; set; }
    }
}