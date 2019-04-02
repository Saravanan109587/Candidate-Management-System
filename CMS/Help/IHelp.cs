using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help
{
   public  class IHelp
    {
        public  IFile file { get; set; }
         public string suggession { get; set; }
    }

    public  class IFile
    {
         public string FileName { get; set; }
         public string Base64String { get; set; }
    }

     
 }
 

