using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneNoteCollaborate.Models
{
    public class Notebook
    {
        private string id { get; set; }
        public string name { get; set; }

        public Notebook(string id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}