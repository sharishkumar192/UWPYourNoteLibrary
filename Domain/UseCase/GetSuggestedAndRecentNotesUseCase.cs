using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPYourNoteLibrary.Data.Managers;
using UWPYourNoteLibrary.Domain.Contract;
using UWPYourNoteLibrary.Models;

namespace UWPYourNoteLibrary.Domain.UseCase
{
    public interface IGetSuggestedAndRecentNotesUseCaseDataManager
    {
        void GetSuggestedAndRecentNotes(string userId, string text, ICallback<GetSuggestedAndRecentNotesUseCaseResponse> callback);
    }
    public class GetSuggestedAndRecentNotesUseCaseRequest
    {
        public string UserId { get; set; }
        public string Text { get; set; }
    }

    public class GetSuggestedAndRecentNotesUseCaseResponse
    {
        public ObservableCollection<Note> List { get; set; }
    }

    public class GetSuggestedAndRecentNotesUseCase : UseCaseBase<GetSuggestedAndRecentNotesUseCaseResponse>
    {
        public GetSuggestedAndRecentNotesUseCaseRequest Request { get; set; }
        public GetSuggestedAndRecentNotesDataManager DataManager { get; set; }
        public ICallback<GetSuggestedAndRecentNotesUseCaseResponse> PresenterCallBack { get; set; }

        public GetSuggestedAndRecentNotesUseCase(GetSuggestedAndRecentNotesUseCaseRequest request, ICallback<GetSuggestedAndRecentNotesUseCaseResponse> callback)
        {
            Request = request;
            DataManager = GetSuggestedAndRecentNotesDataManager.Singleton;
            PresenterCallBack = callback;
        }

        public override void Action()
        {
            DataManager.GetSuggestedAndRecentNotes(Request.UserId, Request.Text, new GetSuggestedAndRecentNotesUseCaseCallBack(this));
        }


        private class GetSuggestedAndRecentNotesUseCaseCallBack : ICallback<GetSuggestedAndRecentNotesUseCaseResponse>
        {
            private GetSuggestedAndRecentNotesUseCase UseCase;

            public GetSuggestedAndRecentNotesUseCaseCallBack(GetSuggestedAndRecentNotesUseCase useCase)
            {
                UseCase = useCase;
            }
            public void onFailure(GetSuggestedAndRecentNotesUseCaseResponse response)
            {
                UseCase?.PresenterCallBack?.onFailure(response);
            }

            public void onSuccess(GetSuggestedAndRecentNotesUseCaseResponse response)
            {
                UseCase?.PresenterCallBack?.onSuccess(response);
            }
        }
    }
}
