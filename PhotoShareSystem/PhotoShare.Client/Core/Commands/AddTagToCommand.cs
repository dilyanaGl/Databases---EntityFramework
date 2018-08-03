using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;

    public class AddTagToCommand : ICommand
    {
        private const string SuccessMessage = "Tag {0} added to {1}!";
        private const string TagOrAlbumNotExist = "Either tag or album do not exist!";
        private const string OperationNotAllowed = "Invalid Credentials!";



        private readonly IAlbumTagService albumTagService;
        private readonly IAlbumService albumService;
        private readonly ITagService tagService;
        private IAlbumRoleService albumRoleService;

        public AddTagToCommand(IAlbumTagService albumTagService, IAlbumService albumService, ITagService tagService, IAlbumRoleService albumRoleService)
        {
            this.albumTagService = albumTagService;
            this.albumService = albumService;
            this.tagService = tagService;
            this.albumRoleService = albumRoleService;
        }

        // AddTagTo <albumName> <tag>
        public string Execute(string[] args)
        {
            var albumName = args[0];
            var tagName = Utilities.TagUtilities.ValidateOrTransform(args[1]);

            if (!albumService.Exists(albumName) || !tagService.Exists(tagName))
            {
                return TagOrAlbumNotExist;
            }

            var album = albumService.ByName<Album>(albumName);
            var tag = tagService.ByName<Tag>(tagName);

            if (albumRoleService.GetOwnerId(album.Id) != Session.CurrentUser.Id)
            {
                return OperationNotAllowed;
            }
                

            albumTagService.AddTagTo(album.Id, tag.Id);

            return String.Format(SuccessMessage, tagName, albumName);
        }
    }
}
