using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Contracts;

    public class ShareAlbumCommand : ICommand
    {
        private const string SuccessMessage = "Username {0} added to album {1} ({2})";
        private const string UserNotFound = "User {0} not found!";
        private const string AlbumNotFound = "Album {0} not found!";
        private const string OperationNotAllowed = "Invalid Credentials!";



        private readonly IAlbumRoleService albumRoleService;
        private readonly IUserService userService;
        private readonly IAlbumService albumService;

        public ShareAlbumCommand(IAlbumRoleService albumService, IUserService userService, IAlbumService albumService1)
        {
            this.albumRoleService = albumService;
            this.userService = userService;
            this.albumService = albumService1;
        }
        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public string Execute(string[] data)
        {
            int albumId = int.Parse(data[0]);
            string username = data[1];
            string permission = data[2];

            if (albumRoleService.GetOwnerId(albumId) != Session.CurrentUser.Id)
            {
                return OperationNotAllowed;
            }

            if (!userService.Exists(username))
            {
                return String.Format(UserNotFound, username);
            }

            if (!albumService.Exists(albumId))
            {
                return String.Format(AlbumNotFound, albumId);
            }

            var user = userService.ByUsername<User>(username);
            var album = albumService.ById<Album>(albumId);

            try
            {
                albumRoleService.PublishAlbumRole(albumId, user.Id, permission);
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            
            return String.Format(SuccessMessage, username, album.Name, permission);

        }
    }
}
