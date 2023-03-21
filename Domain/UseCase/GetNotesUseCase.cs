using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPYourNoteLibrary.Domain.Contract;
using UWPYourNoteLibrary.Models;

using UWPYourNoteLibrary.Data.Managers;

namespace UWPYourNoteLibrary.Domain.UseCase
{
    public interface IGetNotesUseCaseDataManager
    {
        void GetNotes(string userId, string type, bool IsSort, ICallback<GetNotesUseCaseResponse> callback);
        ObservableCollection<Note> GetNotes(string userId);
    }
    public class GetNotesUseCaseRequest
    {
        public string UserId { get; set; }
        public bool IsSort { get; set; }
        public string Type { get; set; }
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
        public ICallback<GetNotesUseCaseResponse> GetNotesVMCallback { get; set; }

        public GetNotesUseCase(GetNotesUseCaseRequest request, ICallback<GetNotesUseCaseResponse> callback)
        {
            Request = request;
            DataManager = GetNotesDataManager.DataManager;
            GetNotesVMCallback = callback;
        }

        public override void Action()
        {
            DataManager.GetNotes(Request.UserId, Request.Type, Request.IsSort, new GetNotesUseCaseCallBack(this));
        }


        private class GetNotesUseCaseCallBack : ICallback<GetNotesUseCaseResponse>
        {
            private GetNotesUseCase _useCase;

            public GetNotesUseCaseCallBack(GetNotesUseCase useCase)
            {
                _useCase = useCase;
            }
            public void onFailure(GetNotesUseCaseResponse result)
            {
                _useCase?.GetNotesVMCallback?.onFailure(result);
            }

            public void onSuccess(GetNotesUseCaseResponse result)
            {
                _useCase?.GetNotesVMCallback?.onSuccess(result);
            }
        }
    }
}
