using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UWPYourNoteLibrary.Domain.UseCase.CanShareNoteUseCase;
using UWPYourNoteLibrary.Domain.Contract;
using UWPYourNoteLibrary.Data.Managers;
using UWPYourNoteLibrary.Models;

namespace UWPYourNoteLibrary.Domain.UseCase
{
    public class CanShareNoteUseCaseResponse
    {
        public bool Result { get; set; }
    }

    public class CanShareNoteUseCaseRequest
    {
        public string UserId { get; set; }
        public long NoteId { get; set; }
      

    }
    public class CanShareNoteUseCase : UseCaseBase<CanShareNoteUseCaseResponse>
    {
        public CanShareNoteUseCaseRequest Request { get; set; }
        public ICallback<CanShareNoteUseCaseResponse> PresenterCallBack { get; set; }

        public interface ICanShareNoteDataManager<CanShareNoteUseCaseResponse>
        {
            void CanShareNote(string userId, long noteId, ICallback<CanShareNoteUseCaseResponse> useCaseCallBack);
        }
        public override void Action()
        {
            DataManager.CanShareNote(Request.UserId, Request.NoteId, new CanShareNoteUseCaseCallBack(this));
        }

        public ICanShareNoteDataManager<CanShareNoteUseCaseResponse> DataManager;
        public CanShareNoteUseCase(CanShareNoteUseCaseRequest request, ICallback<CanShareNoteUseCaseResponse> callback)
        {
            DataManager = CanShareNoteDataManager.Singleton;
            PresenterCallBack = callback;
            Request = request;

        }

        private class CanShareNoteUseCaseCallBack : ICallback<CanShareNoteUseCaseResponse>
        {
            CanShareNoteUseCase UseCase;
            public CanShareNoteUseCaseCallBack(CanShareNoteUseCase useCase)
            {
                UseCase = useCase;
            }

            public void onFailure(CanShareNoteUseCaseResponse result)
            {
                UseCase?.PresenterCallBack?.onFailure(result);
            }

            public void onSuccess(CanShareNoteUseCaseResponse result)
            {
                UseCase?.PresenterCallBack?.onSuccess(result);
            }
        }
    }
}
