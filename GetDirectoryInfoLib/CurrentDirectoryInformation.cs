using System.Collections.Generic;
using System.IO;
using System.Web;

namespace GetDirectoryInfoLib
{
    public class CurrentDirectoryInformation
    {
        private List<MimeTypeDirectoryInformation> mimeTypeInfo = new List<MimeTypeDirectoryInformation>();
        private TreeDirectoryNode directoryTree = new TreeDirectoryNode();

        private int filesCount;

        public void BuildDirectoryTree(string directoryPath)
        {
            var directoryInfo = new DirectoryInfo(directoryPath);
            filesCount = directoryInfo.GetFiles("*", SearchOption.AllDirectories).Length;
            directoryTree = BuildDirectoryTreeRecursive(directoryInfo);
        }

        private TreeDirectoryNode BuildDirectoryTreeRecursive(DirectoryInfo directoryInfo)
        {
            var tree = new TreeDirectoryNode { DirectoryName = directoryInfo.Name, Children = new List<TreeDirectoryNode>(), Size = 0, MimeType = "" };

            if (directoryInfo.GetDirectories().Length > 0)
            {
                foreach (var subDirectory in directoryInfo.GetDirectories())
                {
                    var children = BuildDirectoryTreeRecursive(new DirectoryInfo(subDirectory.FullName));
                    tree.Children.Add(children);
                    tree.Size += children.Size;
                }
            }

            if (directoryInfo.GetFiles().Length > 0)
            {
                foreach (var file in directoryInfo.GetFiles())
                {
                    long fileSize = file.Length;
                    string mimeType = MimeMapping.GetMimeMapping(file.FullName);
                    AddMimeTypeInformation(mimeType, fileSize);
                    tree.Children.Add(new TreeDirectoryNode { DirectoryName = file.Name, Children = new List<TreeDirectoryNode>(), Size = fileSize, MimeType = mimeType });
                    tree.Size += fileSize;
                }
            }
            return tree;
        }

        private void AddMimeTypeInformation(string mimeTypeName, long fileSize)
        {
            int mimeTypeIdx = mimeTypeInfo.FindIndex(i => i.MimeTypeName == mimeTypeName);
            if(mimeTypeIdx != -1)
            {
                mimeTypeInfo[mimeTypeIdx].CountInDirectory += 1;
                mimeTypeInfo[mimeTypeIdx].CountPercentInDirectory += 1.0f / filesCount * 100;
                mimeTypeInfo[mimeTypeIdx].AverageFileSize = (float)(fileSize + mimeTypeInfo[mimeTypeIdx].AverageFileSize * (mimeTypeInfo[mimeTypeIdx].CountInDirectory-1)) / mimeTypeInfo[mimeTypeIdx].CountInDirectory;
            }
            else
            {
                mimeTypeInfo.Add(new MimeTypeDirectoryInformation { MimeTypeName = mimeTypeName, CountInDirectory = 1, CountPercentInDirectory = 1.0f / filesCount * 100, AverageFileSize = fileSize });
            }
        }

        public TreeDirectoryNode GetDirectoryTree()
        {
            return directoryTree;
        }

        public List<MimeTypeDirectoryInformation> GetMimeTypeInfo()
        {
            return mimeTypeInfo;
        }
    }    
}
