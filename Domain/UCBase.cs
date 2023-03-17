using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UWPYourNoteLibrary.Models;

namespace UWPYourNoteLibrary.Domain
{
     public abstract class UCBase<T>
    {
        public abstract void Action();
        public void Execute()
        {
            Task.Run(()=>
            {
                try
                {
                    Action();
                }
                catch(Exception ex ) 
                {
                    Logger.WriteLog(ex.Message);
                }
            }
            );

        }

       
    }

}
