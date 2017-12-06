using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
   public class Cls_ReadConnectionString
    {
       string serverName = string.Empty;
       string databaseName = string.Empty;
       string userId = string.Empty;
       string passWord = string.Empty;
       public string connectionstring = string.Empty;
       bool winNT = true;

       public bool WinNT
       {
           get { return winNT; }
           set { winNT = value; }
       }
       public Cls_ReadConnectionString(string path,bool winNT)
       {
           this.winNT = winNT;
           ReadFileINI(path);
       }
       private string GhepChuoiKetNoi()
       {
           string connnectionstring = string.Empty;
           if(!string.IsNullOrEmpty(serverName))
           {
               connnectionstring += string.Format("server={0}", serverName);
           }
           else
           {
               return null;
           }
           if (!string.IsNullOrEmpty(databaseName))
           {
               connnectionstring += string.Format("; database={0}", databaseName);
           }
           else
           {
               return null;
           }
           if(winNT==true)
           {
               connnectionstring += ";Integrated security=true";
           }
           else
           {
               if (!string.IsNullOrEmpty(userId))
               {
                   connnectionstring += string.Format("; uid={0}", userId);
               }
               else
               {
                   return null;
               }
               if (!string.IsNullOrEmpty(passWord))
               {
                   connnectionstring += string.Format("; pwd={0}", passWord);
               }
               
           }
           return connnectionstring;
       }

       public void ReadFileINI(string path)
       {
           using (StreamReader sr=new StreamReader(path))
           {
               string line = string.Empty;
               while ((line=sr.ReadLine())!=null)
               {
                   if (!string.IsNullOrEmpty(line))
                   {
                       switch (line.Substring(0, line.IndexOf('=')).ToLower())
                       {
                           case "server":
                               serverName = line.Substring(line.IndexOf('=') + 1);
                               break;
                           case "database":
                               databaseName = line.Substring(line.IndexOf('=') + 1);
                               break;
                           case "uid":
                               userId = line.Substring(line.IndexOf('=') + 1);
                               break;
                           case "pwd":
                               passWord = line.Substring(line.IndexOf('=') + 1);
                               break;
                       }
                   }
               }

               //Ghep thong ket noi
               connectionstring = GhepChuoiKetNoi();
           }
       }
    }
}
