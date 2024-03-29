﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPYourNoteLibrary.Data.Handler;
using UWPYourNoteLibrary.Data.Handler.Contract;
using UWPYourNoteLibrary.Domain.Contract;
using UWPYourNoteLibrary.Domain.UseCase;
using UWPYourNoteLibrary.Models;
using static UWPYourNoteLibrary.Domain.UseCase.ValidateCredentialsUseCase;
using UWPYourNoteLibrary.Util;
namespace UWPYourNoteLibrary.Data.Handler
{
    public class ValidateCredentialsDataManager : IValidateCredentialsDataManager<ValidateCredentialsUseCaseResponse>
    {
        private static ValidateCredentialsDataManager validate;
        public IUserDBHandler userDBHandler { get; set; }
        public static ValidateCredentialsDataManager Singleton
        {
            get
            {
                if (validate == null)
                {
                    validate = new ValidateCredentialsDataManager();
                }
                return validate;
            }
        }
        public void ValidateCredentials(string username, string password, ICallback<ValidateCredentialsUseCaseResponse> callback)
        {
            userDBHandler = UserDBHandler.Singleton;
            Models.User user= userDBHandler.ValidateUser(UserUtilities.userTableName, username, password);
            ValidateCredentialsUseCaseResponse response = new ValidateCredentialsUseCaseResponse();
            response.user = user;
            if(user == null)
                callback?.onFailure(response);
            else 
                callback?.onSuccess(response);
        }
           
    }
}
