using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace CopyDel.Models
{
    public class FileList
    {
        private FileEdit fileEdit = new FileEdit();
        private List<CopyList> CheckFileList { get; set; }
        private String Dir { get; set; }
        private long MaxLenghtFile { get; set; }
        private SearchOption serrchOption { get; set; }
        private bool _canselled { get; set; }
        public void Cansel() =>_canselled = true;

        public FileList(string dir, long maxLenghtFile, bool srchOptions = false)
        {
            MaxLenghtFile = maxLenghtFile;
            Dir = dir;
            _canselled = false;
            CheckFileList = new List<CopyList>();
            if(srchOptions) serrchOption = SearchOption.AllDirectories;
            else serrchOption = SearchOption.TopDirectoryOnly;
        }
        public List<CopyList> GetList() => CheckFileList;
        public void MadeList(object param)
        {
            SynchronizationContext context = (SynchronizationContext)param;

            string[] dirs = Directory.GetFiles(Dir, "*.*", serrchOption);
            int dirsLength = dirs.Length;
            if (dirsLength != 0)
            {
                int i = 0;
                foreach (string file in dirs)
                {
                    if (_canselled) break;

                    FileInfo fileInf = new FileInfo(file);
                    if (MaxLenghtFile == 0)
                    {
                        string md5 = fileEdit.ComputeMD5Checksum(file);
                        CheckFileList.Add(new CopyList(file, md5, fileInf.Length));
                    }
                    else
                    {
                        if (fileInf.Length < MaxLenghtFile)
                        {
                            string md5 = fileEdit.ComputeMD5Checksum(file);
                            CheckFileList.Add(new CopyList(file, md5));
                        }
                        else CheckFileList.Add(new CopyList(file, "", fileInf.Length));
                    }
                    context.Send(OnProgressChanged, ++i * 100 / dirsLength);
                }
                context.Send(OnProgressChanged, 100);
            }
        }

        public void OnProgressChanged(object i)
        {
            if (ProcessChanged != null) ProcessChanged((int)i);
        }

        public event Action<int> ProcessChanged;
    }
}
