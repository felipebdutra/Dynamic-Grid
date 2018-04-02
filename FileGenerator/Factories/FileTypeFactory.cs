using FileGenerator.Enum;
using FileGenerator.Interface;
using FileGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileGenerator.Factories
{
    public class FileTypeFactory
    {
        public static IFile Generate(FileTypeEnum fileType)
        {
            switch (fileType)
            {
                case FileTypeEnum.TXT:
                    return new TXTFile();
                case FileTypeEnum.JSON:
                    return new JSONFile();
                case FileTypeEnum.XML:
                    return new XMLFile();
                default:
                    return null;
            }
        }
    }
}