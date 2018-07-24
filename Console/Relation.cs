using System.IO;
using System.Text;

namespace WordsInFiles
{
    public class Relation
    {
        public StringBuilder mySB { get; set; }
        public string SearchedWord { get; set; }
        public FileInfo FileForSearch { get; set; }
    }
}