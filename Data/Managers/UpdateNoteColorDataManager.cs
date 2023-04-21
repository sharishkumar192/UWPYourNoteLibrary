using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UWPYourNoteLibrary.Domain.UseCase.UpdateNoteColorUseCase;
using UWPYourNoteLibrary.Data.Handler.Contract;
using UWPYourNoteLibrary.Data.Handler;
using UWPYourNoteLibrary.Domain.Contract;
using UWPYourNoteLibrary.Domain.UseCase;
using UWPYourNoteLibrary.Util;
using UWPYourNoteLibrary.Models;

namespace UWPYourNoteLibrary.Data.Managers
{
    internal class UpdateNoteColorDataManager : IUpdateNoteColorDataManager<UpdateNoteColorUseCaseResponse>
    {
        public INoteDBHandler noteDBHandler { get; set; }
        private static UpdateNoteColorDataManager checkIf;
        public static UpdateNoteColorDataManager Singleton
        {
            get
            {
                if (checkIf == null)
                {
                    checkIf = new UpdateNoteColorDataManager();
                }
                return checkIf;
            }
        }
        public void UpdateNoteColor(long noteId, long noteColor, string modifiedDay, ICallback<UpdateNoteColorUseCaseResponse> useCaseCallBack)
        {
            noteDBHandler = NoteDBHandler.Singleton;
            bool result = noteDBHandler.UpdateNoteColor(NotesUtilities.notesTableName, noteId, noteColor, modifiedDay);
            
            UpdateNoteColorUseCaseResponse UpdateNoteColorResponse = new UpdateNoteColorUseCaseResponse();
            UpdateNoteColorResponse.Result = result;
            if (result)
                useCaseCallBack?.onSuccess(UpdateNoteColorResponse);
            else
                useCaseCallBack?.onFailure(UpdateNoteColorResponse);

        }

    }
}
