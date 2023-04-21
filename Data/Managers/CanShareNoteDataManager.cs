using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UWPYourNoteLibrary.Domain.UseCase.CanShareNoteUseCase;
using UWPYourNoteLibrary.Data.Handler.Contract;
using UWPYourNoteLibrary.Data.Handler;
using UWPYourNoteLibrary.Domain.Contract;
using UWPYourNoteLibrary.Domain.UseCase;
using UWPYourNoteLibrary.Util;

namespace UWPYourNoteLibrary.Data.Managers
{
    internal class CanShareNoteDataManager : ICanShareNoteDataManager<CanShareNoteUseCaseResponse>
    {
        public INoteDBHandler noteDBHandler { get; set; }
        private static CanShareNoteDataManager checkIf;
        public static CanShareNoteDataManager Singleton
        {
            get
            {
                if (checkIf == null)
                {
                    checkIf = new CanShareNoteDataManager();
                }
                return checkIf;
            }
        }
        public void CanShareNote(string userId, long noteId, ICallback<CanShareNoteUseCaseResponse> useCaseCallBack)
        {
            noteDBHandler = NoteDBHandler.Singleton;
            bool result = noteDBHandler.CanShareNote(NotesUtilities.notesTableName, userId, noteId);

            CanShareNoteUseCaseResponse CanShareNoteResponse = new CanShareNoteUseCaseResponse();
            CanShareNoteResponse.Result = result;
            if (result)
                useCaseCallBack?.onSuccess(CanShareNoteResponse);
            else
                useCaseCallBack?.onFailure(CanShareNoteResponse);

        }

    }
}
