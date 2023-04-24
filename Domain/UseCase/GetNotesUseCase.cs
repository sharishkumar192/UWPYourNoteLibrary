using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPYourNoteLibrary.Domain.Contract;
using UWPYourNoteLibrary.Models;

using UWPYourNoteLibrary.Data.Managers;
using static UWPYourNoteLibrary.Util.NotesUtilities;

namespace UWPYourNoteLibrary.Domain.UseCase
{
    public interface IGetNotesUseCaseDataManager
    {
        void GetNotes(string userId, TypeOfNote type, bool IsSort, ICallback<GetNotesUseCaseResponse> callback);
        ObservableCollection<Note> GetRecentNotes(string userId);
    }
    public class GetNotesUseCaseRequest
    {
        public string UserId { get; set; }
        public bool IsSort { get; set; }
        public TypeOfNote Type { get; set; }
    }

    public class GetNotesUseCaseResponse
    {
        public ObservableCollection<Note> List { get; set; }
            public bool IsSort { get; set; }
        
    }

    public class GetNotesUseCase : UseCaseBase<GetNotesUseCaseResponse>
    {
        public GetNotesUseCaseRequest Request { get; set; }
        public GetNotesDataManager DataManager { get; set; }
        public ICallback<GetNotesUseCaseResponse> PresenterCallBack { get; set; }

        public GetNotesUseCase(GetNotesUseCaseRequest request, ICallback<GetNotesUseCaseResponse> callback)
        {
            Request = request;
            DataManager = GetNotesDataManager.Singleton;
            PresenterCallBack = callback;
        }

        public override void Action()
        {
            DataManager.GetNotes(Request.UserId, Request.Type, Request.IsSort, new GetNotesUseCaseCallBack(this));
        }


        private class GetNotesUseCaseCallBack : ICallback<GetNotesUseCaseResponse>
        {
            private GetNotesUseCase UseCase;

            public GetNotesUseCaseCallBack(GetNotesUseCase useCase)
            {
                UseCase = useCase;
            }
            public void onFailure(GetNotesUseCaseResponse response)
            {
                UseCase?.PresenterCallBack?.onFailure(response);
            }

            public void onSuccess(GetNotesUseCaseResponse response)
            {
                UseCase?.PresenterCallBack?.onSuccess(response);
            }
        }
    }
}
