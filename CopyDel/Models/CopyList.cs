namespace UnmanagedCode.Models
{
    public class CopyList
    {
        public string File { get; set; }
        public string Hesh { get; set; }
        public bool ForDel { get; set; } = false;
        public int Copy { get; set; } = -1;
        public long Size { get; set; } =0;
        public bool IsVirtual { get; set; } = false;

        public CopyList(string file, string hesh, int copy)
        {
            File = file;
            Hesh = hesh;
            Copy = copy;
        }

        public CopyList(string file, string hesh, long fileLength)
        {
            File = file;
            Hesh = hesh;
            Size = fileLength;
        }

        public CopyList(string file, string hesh)
        {
            File = file;
            Hesh = hesh;
        }

        public CopyList(string file, string hesh, bool isVirtual)
        {
            File = file;
            Hesh = hesh;
            IsVirtual = isVirtual;
        }

        public CopyList(string file, string hesh, long fileLength , bool isVirtual)
        {
            File = file;
            Hesh = hesh;
            IsVirtual = isVirtual;
            Size = fileLength;
        }

        public CopyList(string file,  long fileLength)
        {
            File = file;
            Size = fileLength;
        }
    }
}
