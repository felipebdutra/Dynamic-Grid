using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileGenerator.Constants
{
    public static class ErrorMessagesConstant
    {
        public const string Default = "Something went wrong, please try again later.";
        public const string ParseEnum = "Error trying to parse {0} to enum.";
        public const string NotSpecifiedFile = "You have not specified a file.";
        public const string WrongFileUpload = "Something went wrong, make sure you uploaded a file generated here.";
        public const string ExtensionInvalid = "Extension invalid, please insert a JSON valid file.";
    }
}