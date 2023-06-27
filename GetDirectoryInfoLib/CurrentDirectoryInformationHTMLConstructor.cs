using System.Collections.Generic;
using System.Web.Mvc;
using HTMLConstructorLib;

namespace GetDirectoryInfoLib
{
    public class CurrentDirectoryInformationHTMLConstructor
    {
        private HTMLConstructor HTMLConstructor;
        public CurrentDirectoryInformationHTMLConstructor() 
        { 
            HTMLConstructor = new HTMLConstructor();
        }

        public TagBuilder CreateDirectoryTreeHTML(TreeDirectoryNode directoryTree, string ulClass, string liClass)
        {
            if (directoryTree != null)
            {
                TagBuilder directoryTreeHTML = CreateDirectoryTreeHTMLRecursive(directoryTree, ulClass, liClass);
                return directoryTreeHTML;
            }
            else return null;
        }

        private TagBuilder CreateDirectoryTreeHTMLRecursive(TreeDirectoryNode directoryTree, string ulClass, string liClass)
        {
            TagBuilder ulBuilder = HTMLConstructor.CreateUl(ulClass);
            TagBuilder liBuilder = HTMLConstructor.CreateLi(directoryTree.StringInfoConstructor(), liClass);
            ulBuilder.InnerHtml += liBuilder;
            foreach (TreeDirectoryNode tree in directoryTree.Children)
            {
                ulBuilder.InnerHtml += CreateDirectoryTreeHTMLRecursive(tree, ulClass, liClass);
            }
            return ulBuilder;
        }

        //Разделить на 2 метода: создание заголовков и заполнение таблицы. Вызывать оба метода в общем методе по созданию MimeType таблицы
        public TagBuilder CreateMimeTypeHTMLTable(string[] headers, List<MimeTypeDirectoryInformation> mimeTypeInfo, string tableClass, string thClass, string tdClass)
        {
            if (mimeTypeInfo != null && mimeTypeInfo.Count > 0)
            {
                TagBuilder tableBuilder = new TagBuilder("table");
                tableBuilder.AddCssClass(tableClass);
                if (headers != null && headers.Length > 0)
                {
                    TagBuilder trBuilder = new TagBuilder("tr");
                    foreach (string header in headers)
                    {
                        TagBuilder thBuilder = HTMLConstructor.CreateTh(header, thClass);
                        trBuilder.InnerHtml += thBuilder;
                    }
                    tableBuilder.InnerHtml += trBuilder;
                }

                foreach (var mimeType in mimeTypeInfo)
                {
                    TagBuilder trBuilder = new TagBuilder("tr");
                    TagBuilder tdBuilder = HTMLConstructor.CreateTd(mimeType.MimeTypeName, tdClass);
                    trBuilder.InnerHtml += tdBuilder;
                    tdBuilder = HTMLConstructor.CreateTd(mimeType.CountInDirectory.ToString(), tdClass);
                    trBuilder.InnerHtml += tdBuilder;
                    tdBuilder = HTMLConstructor.CreateTd(mimeType.CountPercentInDirectory.ToString(), tdClass);
                    trBuilder.InnerHtml += tdBuilder;
                    tdBuilder = HTMLConstructor.CreateTd(mimeType.AverageFileSize.ToString(), tdClass);
                    trBuilder.InnerHtml += tdBuilder;
                    tableBuilder.InnerHtml += trBuilder;
                }

                return tableBuilder;
            }
            else
            {
                return null;
            }
        }
    }
}
