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
    public interface ICreateNewNoteUseCaseDataManager
    {
        void CreateNewNote(Note newNote, ICallback<CreateNewNoteUseCaseResponse> callback);
    }
    public class CreateNewNoteUseCaseRequest
    {
        public Note NewNote { get; set; }   
    }

    public class CreateNewNoteUseCaseResponse
    {
        public long NoteId { get; set; }
    }

    public class CreateNewNoteUseCase : UseCaseBase<CreateNewNoteUseCaseResponse>
    {
        public CreateNewNoteUseCaseRequest Request { get; set; }
        public CreateNewNoteDataManager DataManager { get; set; }
        public ICallback<CreateNewNoteUseCaseResponse> PresenterCallBack { get; set; }

        public CreateNewNoteUseCase(CreateNewNoteUseCaseRequest request, ICallback<CreateNewNoteUseCaseResponse> callback)
        {
            Request = request;
            DataManager = CreateNewNoteDataManager.Singleton;
            PresenterCallBack = callback;
        }

        public override void Action()
        {
            DataManager.CreateNewNote(Request.NewNote, new CreateNewNoteUseCaseCallBack(this));
        }


        private class CreateNewNoteUseCaseCallBack : ICallback<CreateNewNoteUseCaseResponse>
        {
            private CreateNewNoteUseCase UseCase;

            public CreateNewNoteUseCaseCallBack(CreateNewNoteUseCase useCase)
            {
                UseCase = useCase;
            }
            public void onFailure(CreateNewNoteUseCaseResponse result)
            {
                UseCase?.PresenterCallBack?.onFailure(result);
            }

            public void onSuccess(CreateNewNoteUseCaseResponse result)
            {
                UseCase?.PresenterCallBack?.onSuccess(result);
            }
        }
    }
}
