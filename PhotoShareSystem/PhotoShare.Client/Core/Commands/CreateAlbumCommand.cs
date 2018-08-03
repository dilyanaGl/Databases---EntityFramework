using System.Linq;
using PhotoShare.Models;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Contracts;
    using Services.Contracts;


    public class CreateAlbumCommand : ICommand
    {
        private const string SuccessMessage = "Album {0} successfully created!";
        private const string UserNotFound = "User {0} not found!";
        private const string AlbumExists = "Album {0} exists!";
        private const string OperationNotAllowed = "Invalid Credentials!";


        private readonly IAlbumService albumService;
        private readonly IUserService userService;
        private readonly ITagService tagService;

        public CreateAlbumCommand(IAlbumService albumService, IUserService userService, ITagService tagService)
        {
            this.albumService = albumService;
            this.userService = userService;
            this.tagService = tagService;
        }

        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public string Execute(string[] data)
        {
            string username = data[0];
            string albumName = data[1];
            string bgColor = data[2];
            string[] tags = data.Skip(3).Select(p => Utilities.TagUtilities.ValidateOrTransform(p)).ToArray();
            
            if (username != Session.CurrentUser.Username)
            {
                return OperationNotAllowed;
            }

            if (albumService.Exists(albumName))
            {
                return String.Format(AlbumExists, albumName);
            }

            if (!userService.Exists(username))
            {
                return String.Format(UserNotFound, username);
            }

            var user = userService.ByUsername<User>(username);

            try
            {
                albumService.Create(user.Id, albumName, bgColor, tags);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return String.Format(SuccessMessage, albumName);
        }
    }
}
