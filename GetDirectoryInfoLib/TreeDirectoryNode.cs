using System.Collections.Generic;

namespace GetDirectoryInfoLib
{
    public class TreeDirectoryNode
    {
        public string DirectoryName { get; set; }
        public List<TreeDirectoryNode> Children { get; set; }
        public long Size { get; set; }
        public string MimeType { get; set; }

        public string StringInfoConstructor()
        {
            if (MimeType != "")
            {
                return DirectoryName + ", " + Size + " байт, " + MimeType;
            }
            else
            {
                return DirectoryName + ", " + Size + " байт";
            }
        }
    }
}
