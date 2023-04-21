using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UWPYourNoteLibrary.Domain.UseCase.UpdateNoteColorUseCase;
using UWPYourNoteLibrary.Domain.Contract;
using UWPYourNoteLibrary.Data.Managers;

namespace UWPYourNoteLibrary.Domain.UseCase
{
    public class UpdateNoteColorUseCaseResponse
    {
        public bool Result { get; set; }
    }

    public class UpdateNoteColorUseCaseRequest
    {
        public long NoteId { get; set; }
        public long NoteColor { get; set; }

        public string ModifiedDay { get;set; }

    }
    public class UpdateNoteColorUseCase : UseCaseBase<UpdateNoteColorUseCaseResponse>
    {
        public UpdateNoteColorUseCaseRequest Request { get; set; }
        public ICallback<UpdateNoteColorUseCaseResponse> PresenterCallBack { get; set; }

        public interface IUpdateNoteColorDataManager<UpdateNoteColorUseCaseResponse>
        {
            void UpdateNoteColor(long noteId, long noteColor, string modifiedDay, ICallback<UpdateNoteColorUseCaseResponse> useCaseCallBack);
        }
        public override void Action()
        {
            DataManager.UpdateNoteColor(Request.NoteId, Request.NoteColor, Request.ModifiedDay, new UpdateNoteColorUseCaseCallBack(this));
        }

        public IUpdateNoteColorDataManager<UpdateNoteColorUseCaseResponse> DataManager;
        public UpdateNoteColorUseCase(UpdateNoteColorUseCaseRequest request, ICallback<UpdateNoteColorUseCaseResponse> callback)
        {
            DataManager = UpdateNoteColorDataManager.Singleton;
            PresenterCallBack = callback;
            Request = request;

        }

        private class UpdateNoteColorUseCaseCallBack : ICallback<UpdateNoteColorUseCaseResponse>
        {
            UpdateNoteColorUseCase UseCase;
            public UpdateNoteColorUseCaseCallBack(UpdateNoteColorUseCase useCase)
            {
                UseCase = useCase;
            }

            public void onFailure(UpdateNoteColorUseCaseResponse result)
            {
                UseCase?.PresenterCallBack?.onFailure(result);
            }

            public void onSuccess(UpdateNoteColorUseCaseResponse result)
            {
                UseCase?.PresenterCallBack?.onSuccess(result);
            }
        }
    }
}
