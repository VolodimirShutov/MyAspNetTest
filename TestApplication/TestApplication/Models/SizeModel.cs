using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeforeTest.Models
{
    public class SizeModel
    {
        public int lessTen { get; set; }
        public int ten { get; set; }
        public int moreTen { get; set; }

        public string parentPath { get; set; }
        public string fullPath { get; set; }

        public List<FileModel> files { get; set; }
        public List<DirectoriesModel> directories { get; set; }

        public void initClass()
        {
            lessTen = 0;
            ten = 0;
            moreTen = 0;
        }
    }
}