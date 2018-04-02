using FileGenerator.Enum;
using FileGenerator.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileGenerator.Models
{
    public class JSONFile : IFile
    {
        public FileTypeEnum Type
        {
            get { return FileTypeEnum.JSON; }
            set { Type = value; }
        }

        public string ContentType { get { return "application/json; charset=utf-8"; } }
        public string ContentDisposition { get { return "attachment;filename=\"export.json\""; } }

        //Default extension is JSON, doesn't need to be converted
        public string ConvertFile(string json)
        {
            StrFile = json;
            return json;
        }
        public string StrFile { get; set; }
    }
}