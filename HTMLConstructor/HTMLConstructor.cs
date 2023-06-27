
using System.Web.Mvc;

namespace HTMLConstructorLib
{
    public class HTMLConstructor
    {

        public TagBuilder CreateUl(string ulClass)
        {
            TagBuilder ulBuilder = new TagBuilder("ul");
            AddClass(ulClass, ulBuilder);
            return ulBuilder;
        }

        public TagBuilder CreateLi(string innerText, string liClass)
        {
            TagBuilder liBuilder = new TagBuilder("li");
            AddClass(liClass, liBuilder);
            AddInnerText(innerText, liBuilder);
            return liBuilder;
        }

        public TagBuilder CreateTh(string header, string thClass)
        {
            TagBuilder thBuilder = new TagBuilder("th");
            AddClass(thClass, thBuilder);
            AddInnerText(header, thBuilder);
            return thBuilder;
        }

        public TagBuilder CreateTd(string innerText, string tdClass)
        {
            TagBuilder tdBuilder = new TagBuilder("td");
            AddClass(tdClass, tdBuilder);
            AddInnerText(innerText, tdBuilder);
            return tdBuilder;
        }

        public void AddClass(string cssClass, TagBuilder tagBuilder)
        {
            if (cssClass != null)
            {
                tagBuilder.AddCssClass(cssClass);
            }
        }

        public void AddInnerText(string innerText, TagBuilder tagBuilder)
        {
            if (innerText != null)
            {
                tagBuilder.SetInnerText(innerText);
            }
        }
    }
}
