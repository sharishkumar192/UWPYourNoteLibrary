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
    public interface IDeleteNoteUseCaseDataManager
    {
        void DeleteNote(long noteId, ICallback<DeleteNoteUseCaseResponse> callback);
    }
    public class DeleteNoteUseCaseRequest
    {
        public long NoteId { get; set; }
    }

    public class DeleteNoteUseCaseResponse
    {
        public bool Result { get; set; }
    }

    public class DeleteNoteUseCase : UseCaseBase<DeleteNoteUseCaseResponse>
    {
        public DeleteNoteUseCaseRequest Request { get; set; }
        public DeleteNoteDataManager DataManager { get; set; }
        public ICallback<DeleteNoteUseCaseResponse> PresenterCallBack { get; set; }

        public DeleteNoteUseCase(DeleteNoteUseCaseRequest request, ICallback<DeleteNoteUseCaseResponse> callback)
        {
            Request = request;
            DataManager = DeleteNoteDataManager.Singleton;
            PresenterCallBack = callback;
        }

        public override void Action()
        {
            DataManager.DeleteNote(Request.NoteId, new DeleteNoteUseCaseCallBack(this));
        }


        private class DeleteNoteUseCaseCallBack : ICallback<DeleteNoteUseCaseResponse>
        {
            private DeleteNoteUseCase UseCase;

            public DeleteNoteUseCaseCallBack(DeleteNoteUseCase useCase)
            {
                UseCase = useCase;
            }
            public void onFailure(DeleteNoteUseCaseResponse result)
            {
                UseCase?.PresenterCallBack?.onFailure(result);
            }

            public void onSuccess(DeleteNoteUseCaseResponse result)
            {
                UseCase?.PresenterCallBack?.onSuccess(result);
            }
        }
    }
}
