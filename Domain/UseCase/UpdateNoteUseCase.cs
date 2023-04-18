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
    public interface IUpdateNoteUseCaseDataManager
    {
        void UpdateNote(Note newNote, bool isTitleChange, bool isContentChange, ICallback<UpdateNoteUseCaseResponse> callback);
    }
    public class UpdateNoteUseCaseRequest
    {
        public Note NoteToUpdate { get; set; }
        public bool IsTitleChanged { get; set; }
        public bool IsContentChanged { get; set; }
    }

    public class UpdateNoteUseCaseResponse
    {
        public bool Result { get; set; }   
    }

    public class UpdateNoteUseCase : UseCaseBase<UpdateNoteUseCaseResponse>
    {
        public UpdateNoteUseCaseRequest Request { get; set; }
        public UpdateNoteDataManager DataManager { get; set; }
        public ICallback<UpdateNoteUseCaseResponse> PresenterCallBack { get; set; }

        public UpdateNoteUseCase(UpdateNoteUseCaseRequest request, ICallback<UpdateNoteUseCaseResponse> callback)
        {
            Request = request;
            DataManager = UpdateNoteDataManager.Singleton;
            PresenterCallBack = callback;
        }

        public override void Action()
        {
            DataManager.UpdateNote(Request.NoteToUpdate, Request.IsTitleChanged, Request.IsContentChanged, new UpdateNoteUseCaseCallBack(this));
        }


        private class UpdateNoteUseCaseCallBack : ICallback<UpdateNoteUseCaseResponse>
        {
            private UpdateNoteUseCase UseCase;

            public UpdateNoteUseCaseCallBack(UpdateNoteUseCase useCase)
            {
                UseCase = useCase;
            }
            public void onFailure(UpdateNoteUseCaseResponse response)
            {
                UseCase?.PresenterCallBack?.onFailure(response);
            }

            public void onSuccess(UpdateNoteUseCaseResponse response)
            {
                UseCase?.PresenterCallBack?.onSuccess(response);
            }
        }
    }
}
