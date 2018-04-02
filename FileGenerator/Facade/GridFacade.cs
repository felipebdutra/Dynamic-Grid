using FileGenerator.Constants;
using FileGenerator.Enum;
using FileGenerator.Helper;
using FileGenerator.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FileGenerator.Facade
{
    public class GridFacade
    {
        public string UpdateGrid(string aTable)
        {
            try
            {
                List<Client> clients = RetrieveJSONGrid(aTable);

                clients = OrderGrid(clients);

                clients = UpdatePosition(clients);

                return JsonConvert.SerializeObject(clients);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string UpdatePoint(string aTable, int aQntPoint, int aIndex)
        {
            try
            {
                List<Client> clients = new List<Client>();

                clients = RetrieveJSONGrid(aTable);

                clients[aIndex].Points += aQntPoint;

                clients = OrderGrid(clients);

                clients = UpdatePosition(clients);

                return JsonConvert.SerializeObject(clients);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void PrepareFile(string aTable, string fileType)
        {
            FileTypeEnum fileTypeVal;

            try
            {
                if (System.Enum.TryParse<FileTypeEnum>(fileType, out fileTypeVal))
                {
                    FileHelper.File = Factories.FileTypeFactory.Generate(fileTypeVal);
                    FileHelper.File.ConvertFile(aTable);
                }
                else
                    throw new Exception(string.Format(ErrorMessagesConstant.ParseEnum, fileType));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public byte[] GetFileBytes(HttpPostedFileBase file)
        {
            BinaryReader b = new BinaryReader(file.InputStream);
            return b.ReadBytes(file.ContentLength);
        }

        private List<Client> RetrieveJSONGrid(string aTable) => JsonConvert.DeserializeObject<List<Client>>(aTable);

        public List<Client> OrderGrid(List<Client> aClients) => aClients.OrderByDescending(o => o.Points).ToList();

        public List<Client> UpdatePosition(List<Client> aClients)
        {
            aClients.ForEach(f => f.Position = aClients.IndexOf(f) + 1);
            return aClients;
        }

    }
}