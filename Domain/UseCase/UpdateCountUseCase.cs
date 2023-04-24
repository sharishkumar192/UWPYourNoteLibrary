using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UWPYourNoteLibrary.Domain.UseCase.UpdateCountUseCase;
using UWPYourNoteLibrary.Domain.Contract;
using UWPYourNoteLibrary.Data.Managers;

namespace UWPYourNoteLibrary.Domain.UseCase
{
    public class UpdateCountUseCaseResponse
    {
        public bool Result { get; set; }
    }

    public class UpdateCountUseCaseRequest
    {
        public long SearchCount { get; set; }
        public long NoteId { get; set; }

    }
    public class UpdateCountUseCase : UseCaseBase<UpdateCountUseCaseResponse>
    {
        public UpdateCountUseCaseRequest Request { get; set; }
        public ICallback<UpdateCountUseCaseResponse> PresenterCallBack { get; set; }

        public interface IUpdateCountDataManager<UpdateCountUseCaseResponse>
        {
            void UpdateNoteCount(long searchCount, long noteId, ICallback<UpdateCountUseCaseResponse> useCaseCallBack);
        }
        public override void Action()
        {
            DataManager.UpdateNoteCount(Request.SearchCount, Request.NoteId, new UpdateCountUseCaseCallBack(this));
        }

        public IUpdateCountDataManager<UpdateCountUseCaseResponse> DataManager;
        public UpdateCountUseCase(UpdateCountUseCaseRequest request, ICallback<UpdateCountUseCaseResponse> callback)
        {
            DataManager = UpdateCountDataManager.Singleton;
            PresenterCallBack = callback;
            Request = request;

        }

        private class UpdateCountUseCaseCallBack : ICallback<UpdateCountUseCaseResponse>
        {
            UpdateCountUseCase UseCase;
            public UpdateCountUseCaseCallBack(UpdateCountUseCase useCase)
            {
                UseCase = useCase;
            }

            public void onFailure(UpdateCountUseCaseResponse response)
            {
                UseCase?.PresenterCallBack?.onFailure(response);
            }

            public void onSuccess(UpdateCountUseCaseResponse response)
            {
                UseCase?.PresenterCallBack?.onSuccess(response);
            }
        }
    }

}
