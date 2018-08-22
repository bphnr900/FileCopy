using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace FileCopy
{
    public class GetXML
    {
        public void Sample()
        {
            IEnumerable<XElement> files = XElement.Load(@"C:\Users\bphnr\source\repos\FileCopy\FileCopy\FileList.xml").Elements("Folders");
            
            foreach(XElement file in files)
            {
                Console.WriteLine(file.Elements("FolderName"));
            }
            

        }
    }
}
