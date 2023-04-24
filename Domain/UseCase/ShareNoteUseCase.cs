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
    public interface IShareNoteUseCaseDataManager
    {
        void ShareNote(long noteId, string sharedUserId, ICallback<ShareNoteUseCaseResponse> callback);
    }
    public class ShareNoteUseCaseRequest
    {
        public long NoteId { get; set; }
        public string SharedUserID { get; set; }
    }

    public class ShareNoteUseCaseResponse
    {
        public bool Result { get; set; }
    }

    public class ShareNoteUseCase : UseCaseBase<ShareNoteUseCaseResponse>
    {
        public ShareNoteUseCaseRequest Request { get; set; }
        public ShareNoteDataManager DataManager { get; set; }
        public ICallback<ShareNoteUseCaseResponse> PresenterCallBack { get; set; }

        public ShareNoteUseCase(ShareNoteUseCaseRequest request, ICallback<ShareNoteUseCaseResponse> callback)
        {
            Request = request;
            DataManager = ShareNoteDataManager.Singleton;
            PresenterCallBack = callback;
        }

        public override void Action()
        {
            DataManager.ShareNote(Request.NoteId, Request.SharedUserID, new ShareNoteUseCaseCallBack(this));
        }


        private class ShareNoteUseCaseCallBack : ICallback<ShareNoteUseCaseResponse>
        {
            private ShareNoteUseCase UseCase;

            public ShareNoteUseCaseCallBack(ShareNoteUseCase useCase)
            {
                UseCase = useCase;
            }
            public void onFailure(ShareNoteUseCaseResponse response)
            {
                UseCase?.PresenterCallBack?.onFailure(response);
            }

            public void onSuccess(ShareNoteUseCaseResponse response)
            {
                UseCase?.PresenterCallBack?.onSuccess(response);
            }
        }
    }
}
