using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPYourNoteLibrary.Models;
namespace UWPYourNoteLibrary.Notification
{
    public class NotificationManager
    {
        public static event Action<Note> UpdateNoteSucceeded;
        public static event Action DeleteNoteSucceeded;

        public static void NotifyUpdateNoteSucceeded(Note updatedNote)
        {
            UpdateNoteSucceeded?.Invoke(updatedNote);
        }

        public static void NotifyDeleteNoteSucceeded()
        {
            DeleteNoteSucceeded?.Invoke();
        }

    }
}
