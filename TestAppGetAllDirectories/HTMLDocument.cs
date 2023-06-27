using System.IO;
using System.Text;

namespace TestAppGetAllDirectories
{
    internal static class HTMLDocument
    {

        public static void CreateResultHTML(string htmlText, string path)
        {
            using (FileStream file = new FileStream(path, FileMode.Create))
            {
                byte[] htmlByte = Encoding.Default.GetBytes(htmlText);
                file.Write(htmlByte, 0, htmlByte.Length);
            }
        }
    }
}
