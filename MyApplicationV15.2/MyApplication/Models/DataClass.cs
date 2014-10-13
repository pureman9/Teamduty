using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApplication.Models;
using System.Text;
using System.Data;
using MyApplication.Repository;
using System.Web.Security;
using System.IO;
using PagedList;
using System.Diagnostics;
using System.Web.Hosting;

namespace MyApplication.Models
{
    public class DataClasses
    {
        public List<FileNames> GetFiles()
        {

            List<FileNames> lstFiles = new List<FileNames>();
            var path = HostingEnvironment.MapPath("~/files");
            var dirInfo = new DirectoryInfo(path);
            int i = 0;
            if (dirInfo.GetFiles() != null)
            {
                foreach (var item in dirInfo.GetFiles().OrderBy(u => u.LastWriteTime))
                {

                    lstFiles.Add(new FileNames()
                    {
                            
                        FileId = i + 1,
                        FileName = item.Name,
                        FilePath = dirInfo.FullName + @"\" + item.Name,
                         
                    });
                    i = i + 1;
                }
            }
            else
            {
                
            }

            return lstFiles;
        }

        }


      
 

    public class FileNames
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    //    public byte[] FileImageData { get; set; }
    }

     
      
}