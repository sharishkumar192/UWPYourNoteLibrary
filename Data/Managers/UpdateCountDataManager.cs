using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPYourNoteLibrary.Data.Handler.Contract;
using UWPYourNoteLibrary.Data.Handler;
using UWPYourNoteLibrary.Domain.Contract;
using UWPYourNoteLibrary.Domain.UseCase;
using UWPYourNoteLibrary.Util;
using static UWPYourNoteLibrary.Domain.UseCase.UpdateCountUseCase;

namespace UWPYourNoteLibrary.Data.Managers
{
    internal class UpdateCountDataManager : IUpdateCountDataManager<UpdateCountUseCaseResponse>
    {
        public INoteDBHandler noteDBHandler { get; set; }
        private static UpdateCountDataManager checkIf;
        public static UpdateCountDataManager Singleton
        {
            get
            {
                if (checkIf == null)
                {
                    checkIf = new UpdateCountDataManager();
                }
                return checkIf;
            }
        }

        public void UpdateNoteCount(long searchCount, long noteId, ICallback<UpdateCountUseCaseResponse> useCaseCallBack)
        {
            noteDBHandler = NoteDBHandler.Singleton;

            bool result = noteDBHandler.UpdateNoteCount(NotesUtilities.notesTableName, searchCount, noteId);
         
            UpdateCountUseCaseResponse updateCountResponse = new UpdateCountUseCaseResponse();
            updateCountResponse.Result = result;
            if (result)
                useCaseCallBack?.onSuccess(updateCountResponse);
            else
                useCaseCallBack?.onFailure(updateCountResponse);

        }
      
    }
}
