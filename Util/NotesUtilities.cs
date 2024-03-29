﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using UWPYourNoteLibrary.Domain;
using UWPYourNoteLibrary.Domain.Contract;
using UWPYourNoteLibrary.Domain.UseCase;
using UWPYourNoteLibrary.Models;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace UWPYourNoteLibrary.Util
{
    public class NotesUtilities
    {
        public static string notesTableName = "NotesTable";
        public static string sharedTableName = "ShareTable";

        public enum TypeOfNote
        {
            PersonalNotes,
            SharedNotes,
            AllNotes
        }

        public enum AutoSaveTimer
        {
            TimeFrequency = 5
        }

        public enum NoteBackgroundColor
        {
            BackgroundPink,
            BackgroundGreen,
            BackgroundYellow,
            BackgroundBlue
        }

        public static Dictionary<int, string> noteColorStyle = new Dictionary<int, string>()
        {
            {0, "RedNoteColor" },
              {1, "GreenNoteColor" },
                {2, "YellowNoteColor" },
                  {3, "BlueNoteColor" }

        };
        public static Dictionary<int, string> noteColorButtonStyle = new Dictionary<int, string>()
        {
            {0, "RedColorNoteButton" },
              {1, "GreenColorNoteButton" },
                {2, "YellowColorNoteButton" },
                  {3, "BlueColorNoteButton" }

        };

        public static Dictionary<int, string> noteShareBackgroundStyle = new Dictionary<int, string>()
        {
            {0, "#c4969c" },
              {1, "#9ab48e" },
                {2, "#cabe8a" },
                  {3, "#9bbaca" }

        };





        public static SolidColorBrush GetSolidColorBrush(long value)
        {
            int index = (int)value;
            List<string> color = new List<string>()
        { "#f8bec5", "#c6e8b7", "#fdefad", "#c3e9fd"};
            string hex = color[index];
            hex = hex.Replace("#", string.Empty);
            byte r = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
            SolidColorBrush myBrush = new SolidColorBrush(Windows.UI.Color.FromArgb((byte)255, r, g, b));
            return myBrush;
        }

        public static SolidColorBrush GetSolidColorBrush(string color)
        {

            string hex = color;
            hex = hex.Replace("#", string.Empty);
            byte r = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
            SolidColorBrush myBrush = new SolidColorBrush(Windows.UI.Color.FromArgb((byte)255, r, g, b));
            return myBrush;
        }


        //----Note Font Background

        public static void FontBackgroundClick(object sender, RoutedEventArgs e)
        {
            try
            {
                RichEditBox box = (RichEditBox)sender;
                Windows.UI.Text.ITextSelection selectedText = box.Document.Selection;
                if (selectedText != null)
                {
                    Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                    if (charFormatting.BackgroundColor.R == 0 && charFormatting.BackgroundColor.G == 0 && charFormatting.BackgroundColor.B == 0)
                    {
                        charFormatting.BackgroundColor = Windows.UI.Color.FromArgb(255, 255, 255, 255);
                        charFormatting.ForegroundColor = Windows.UI.Color.FromArgb(0, 0, 0, 0);
                    }
                    else
                    {
                        charFormatting.BackgroundColor = Windows.UI.Color.FromArgb(0, 0, 0, 0);
                        charFormatting.ForegroundColor = Windows.UI.Color.FromArgb(0, 255, 255, 255);
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message);
            }
        }

        //----Note Font Increase
        public static void FontIncreaseFunc(Windows.UI.Text.ITextSelection selectedText, float limit, float value)
        {
            try
            {
                Windows.UI.Text.ITextCharacterFormat charFormatting;
                if (selectedText != null)
                {
                    charFormatting = selectedText.CharacterFormat;
                    string text = selectedText.Text;
                    float size = charFormatting.Size;
                    if (!String.IsNullOrEmpty(text) && size < limit)
                    {
                        charFormatting.Size += value;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message);
            }

        }
        public static void FontIncreaseClick(object sender, RoutedEventArgs e)
        {
            RichEditBox box = (RichEditBox)sender;
            FontIncreaseFunc(box.Document.Selection, (float)15, (float)0.5);
        }

        //----Note Font Decrease
        public static void FontDecreaseFunc(Windows.UI.Text.ITextSelection selectedText, float limit, float value)
        {

            try {
                Windows.UI.Text.ITextCharacterFormat charFormatting;
                if (selectedText != null)
                {
                    charFormatting = selectedText.CharacterFormat;
                    string text = selectedText.Text;
                    float size = charFormatting.Size;
                    if (!String.IsNullOrEmpty(text) && size > limit)
                    {
                        charFormatting.Size -= value;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message);
            }
        }

        public static void FontDecreaseClick(object sender, RoutedEventArgs e)
        {

            RichEditBox box = (RichEditBox)sender;

            FontDecreaseFunc(box.Document.Selection, (float)7.5, (float)0.5);
        }

        //----Note Small Caps
        public static void SmallCapsClick(object sender, RoutedEventArgs e)
        {

            try {
                RichEditBox box = (RichEditBox)sender;
                Windows.UI.Text.ITextSelection selectedText = box.Document.Selection;
                if (selectedText != null)
                {
                    Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                    charFormatting.SmallCaps = Windows.UI.Text.FormatEffect.Toggle;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message);
            }
        }

        //----Note All Caps
        public static void AllCapsClick(object sender, RoutedEventArgs e)
        {


            try {
                RichEditBox box = (RichEditBox)sender;
                Windows.UI.Text.ITextSelection selectedText = box.Document.Selection;
                if (selectedText != null)
                {
                    Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                    charFormatting.AllCaps = Windows.UI.Text.FormatEffect.Toggle;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message);
            }

        }

        //----Note Strikethrough
        public static void StrikethroughClick(object sender, RoutedEventArgs e)
        {


            try {
                RichEditBox box = (RichEditBox)sender;
                Windows.UI.Text.ITextSelection selectedText = box.Document.Selection;
                if (selectedText != null)
                {
                    Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                    charFormatting.Strikethrough = Windows.UI.Text.FormatEffect.Toggle;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message);
            }
        }


        public static void UpdateNote(Note noteToUpdate, bool titleChange, bool contentChange, ICallback<UpdateNoteUseCaseResponse> callback)
        {
            UpdateNoteUseCaseRequest request = new UpdateNoteUseCaseRequest();
            request.NoteToUpdate = noteToUpdate;
            request.IsTitleChanged = titleChange;
            request.IsContentChanged = contentChange;
            UpdateNoteUseCase usecase = new UpdateNoteUseCase(request, callback);
            usecase.Execute();
        }

        public static void DeleteNote(long noteId, ICallback<DeleteNoteUseCaseResponse> callback)
        {
            DeleteNoteUseCaseRequest request = new DeleteNoteUseCaseRequest();
            request.NoteId = noteId;
            DeleteNoteUseCase usecase = new DeleteNoteUseCase(request, callback);
            usecase.Execute();
        }


        public static void ShareNote(string sharedUserId, long noteId, ICallback<ShareNoteUseCaseResponse> callback)
        {
            ShareNoteUseCaseRequest request = new ShareNoteUseCaseRequest();
            request.NoteId = noteId;
            request.SharedUserID = sharedUserId;
            ShareNoteUseCase usecase = new ShareNoteUseCase(request, callback);
            usecase.Execute();


        }


        public static void ValidUsersToShare(string userId, long displayNoteId, ICallback<ValidUsersToShareUseCaseResponse> callback)
        {
            ValidUsersToShareUseCaseRequest request = new ValidUsersToShareUseCaseRequest();
            request.UserId = userId;
            request.NoteId = displayNoteId;
            ValidUsersToShareUseCase usecase = new ValidUsersToShareUseCase(request, callback);
            usecase.Execute();
        }

        public static void CanShareNote(string userId, long noteId, ICallback<CanShareNoteUseCaseResponse> callback)
        {
            CanShareNoteUseCaseRequest request = new CanShareNoteUseCaseRequest();
            request.UserId = userId;
            request.NoteId = noteId;
            CanShareNoteUseCase usecase = new CanShareNoteUseCase(request, callback);
            usecase.Execute();
        }

        public static void UpdateNoteColor(long noteId, long noteColor, string modifiedDay, ICallback<UpdateNoteColorUseCaseResponse> callback)
        {
            UpdateNoteColorUseCaseRequest request = new UpdateNoteColorUseCaseRequest();
            request.NoteId = noteId;
            request.NoteColor = noteColor;
            request.ModifiedDay = modifiedDay;
            UpdateNoteColorUseCase usecase = new UpdateNoteColorUseCase(request, callback);
            usecase.Execute();

        }


        public static void UpdateCount(long searchCount, long noteId, ICallback<UpdateCountUseCaseResponse> callback)
        {
            UpdateCountUseCaseRequest request = new UpdateCountUseCaseRequest();
            request.SearchCount = searchCount;
            request.NoteId = noteId;
            UpdateCountUseCase usecase = new UpdateCountUseCase(request, callback);
            usecase.Execute();
        }
}
}
