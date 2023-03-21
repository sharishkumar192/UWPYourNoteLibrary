﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPYourNoteLibrary.Data.Handler;
using UWPYourNoteLibrary.Domain;
using UWPYourNoteLibrary.Domain.Contract;
using UWPYourNoteLibrary.Models;
using UWPYourNoteLibrary.Data.Handler.Contract;
using static UWPYourNoteLibrary.Domain.CheckIfUserExistsUseCase;
namespace UWPYourNoteLibrary.Data.Managers

{
    internal class CheckIfUserExistsEmailDataManager : ICheckIfUserExistsEmailDataManager<CheckIfUserExistsUseCaseResponse>
    {
        public IUserDBHandler userDBHandler { get; set; }
        private static CheckIfUserExistsEmailDataManager checkIf;
        public  static CheckIfUserExistsEmailDataManager DataManager
        {
            get
            {
                if(checkIf == null) 
                {
                    checkIf = new CheckIfUserExistsEmailDataManager();
                }
                return checkIf;
            }
        }
        public void CheckIfExistingEmail(string email, ICallback<CheckIfUserExistsUseCaseResponse> useCaseCallBack)
        {
            userDBHandler = UserDBHandler.Handler;

            bool isExist = userDBHandler.CheckIfExistingEmail(DBCreation.userTableName, email);
            CheckIfUserExistsUseCaseResponse checkIfExistingEmailUseCaseRequest = new CheckIfUserExistsUseCaseResponse();
            checkIfExistingEmailUseCaseRequest.IsExists = isExist;
            if (isExist)
                useCaseCallBack?.onSuccess(checkIfExistingEmailUseCaseRequest);
            else
                useCaseCallBack?.onFailure(checkIfExistingEmailUseCaseRequest);
        }
    }
}
