using FileGenerator.Constants;
using FileGenerator.Enum;
using FileGenerator.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileGenerator.Helpers
{
    public class FileHelper
    {
        public static IFile File
        {
            get; set;
        }

        public static bool IsExtensionValid(string aFileName)
        {
            return ValidExtensionConstant.Extensions.Contains(aFileName.Split('.').Last().ToUpper());
        }
    }
}