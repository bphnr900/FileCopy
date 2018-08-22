using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Xml;

namespace FileCopy
{
    public class Program
    {
        static void Main(string[] args)
        {
            //xml読み込み
            //IEnumerable<XElement> Folders = XElement.Load(@"C:\Users\bphnr\source\repos\FileCopy\FileCopy\FileList.xml").Elements("Folder");
            IEnumerable<XElement> Folders = XElement.Load("FileList.xml").Elements("Folder");

            //読み込み結果を確認
            foreach (XElement folder in Folders)
            {
                //コピー元、コピー先のディレクトリを取得
                string BeforePath = folder.Element("SourceDirectory").Value;
                string AfterPath = folder.Element("DestinationDirectory").Value;

                //取得したパスをもとにコピー
                DirectoryCopy(BeforePath, AfterPath);
            }
        }

        //ディレクトリのコピー
        public static void DirectoryCopy(string SourcePath,string DestinationPath)
        {
            DirectoryInfo sourceDirectory = new DirectoryInfo(SourcePath);
            DirectoryInfo destinationDirectory = new DirectoryInfo(DestinationPath);
            //コピー先のディレクトリがなければ作成する
            if(destinationDirectory.Exists == false)
            {
                destinationDirectory.Create();
                destinationDirectory.Attributes = sourceDirectory.Attributes;
            }
            //ファイルのコピー
            foreach(FileInfo fileinfo in sourceDirectory.GetFiles())
            {
                //同じファイルが存在していたら常に上書きする
                fileinfo.CopyTo(destinationDirectory.FullName + @"\" + fileinfo.Name, true);
            }
            //ディレクトリのコピー(再起を利用)
            foreach(System.IO.DirectoryInfo directoryInfo in sourceDirectory.GetDirectories())
            {
                DirectoryCopy(directoryInfo.FullName, destinationDirectory.FullName + @"\" + directoryInfo.Name);
            }
        }
    }
}
