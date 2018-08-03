using System;
using System.Windows.Input;
using PhotoShare.Services.Contracts;
using ICommand = PhotoShare.Client.Core.Contracts.ICommand;

namespace PhotoShare.Client.Core.Commands
{
    public class AddTagCommand : ICommand
    {
        private const string SuccessMessage = "Tag {0} was added successfully!";
        private const string TagExists = "Tag {0} exists!";
        private const string OperationNotAllowed = "Invalid Credentials!";

        private readonly ITagService tagService;


        public AddTagCommand(ITagService tagService)
        {
            this.tagService = tagService;
        }

        public string Execute(string[] args)
        {
            var tagName = Utilities.TagUtilities.ValidateOrTransform(args[0]);

            if (Session.CurrentUser == null)
            {
                return OperationNotAllowed;
            }

            if (tagService.Exists(tagName))
            {
                return String.Format(TagExists, tagName);
            }

            tagService.AddTag(tagName);

            return String.Format(SuccessMessage, tagName);
        }
    }
}
