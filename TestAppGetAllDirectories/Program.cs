using GetDirectoryInfoLib;
using System.IO;
using System.Web.Mvc;

namespace TestAppGetAllDirectories
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Data data = new Data();
            Config config = data.GetConfig();

            TagBuilder head = new TagBuilder("head");

            TagBuilder meta = new TagBuilder("meta");
            meta.Attributes.Add("charset", "ansi");
            head.InnerHtml += meta;

            TagBuilder title = new TagBuilder("title");
            title.SetInnerText(config.HTML_TYTLE);
            head.InnerHtml += title;


            string styleStr = data.GetStyle();
            TagBuilder style = new TagBuilder("style");
            style.Attributes.Add("type", "text/css");
            style.SetInnerText(styleStr);
            head.InnerHtml += style;


            CurrentDirectoryInformation getMimeTypeInfo = new CurrentDirectoryInformation();
            string currentDirectory = Directory.GetCurrentDirectory();
            getMimeTypeInfo.BuildDirectoryTree(currentDirectory);
            CurrentDirectoryInformationHTMLConstructor htmlConstructor = new CurrentDirectoryInformationHTMLConstructor();
            TagBuilder htmlTree = htmlConstructor.CreateDirectoryTreeHTML(getMimeTypeInfo.GetDirectoryTree(), config.UL_CLASS, config.LI_CLASS);
            TagBuilder htmlTable = htmlConstructor.CreateMimeTypeHTMLTable(config.MIME_TYPE_TABLE_HEADERS, getMimeTypeInfo.GetMimeTypeInfo(), config.TABLE_CLASS, config.TH_CLASS, config.TD_CLASS);

            TagBuilder body = new TagBuilder("body");
            body.InnerHtml += htmlTree;
            body.InnerHtml += htmlTable;

            TagBuilder html = new TagBuilder("html");
            html.InnerHtml += head;
            html.InnerHtml += body;

            HTMLDocument.CreateResultHTML(html.ToString(), config.PATH_TO_RESULT_HTML);
        }
    }
}
