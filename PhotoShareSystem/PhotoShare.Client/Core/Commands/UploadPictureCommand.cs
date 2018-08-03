namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Dtos;
    using Contracts;
    using Services.Contracts;

    public class UploadPictureCommand : ICommand
    {
        private const string OperationNotAllowed = "Invalid Credentials!";

        private readonly IPictureService pictureService;
        private readonly IAlbumService albumService;
        private IAlbumRoleService albumRoleService;

        public UploadPictureCommand(IPictureService pictureService, IAlbumService albumService, IAlbumRoleService albumRoleService)
        {
            this.pictureService = pictureService;
            this.albumService = albumService;
            this.albumRoleService = albumRoleService;
        }

        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public string Execute(string[] data)
        {
            string albumName = data[0];
            string pictureTitle = data[1];
            string path = data[2];

            var albumExists = this.albumService.Exists(albumName);

            if (!albumExists)
            {
                throw new ArgumentException($"Album {albumName} not found!");
            }

            var albumId = this.albumService.ByName<AlbumDto>(albumName).Id;

            if (albumRoleService.GetOwnerId(albumId) != Session.CurrentUser.Id)
            {
                return OperationNotAllowed;
            }

            var picture = this.pictureService.Create(albumId, pictureTitle, path);

            return $"Picture {pictureTitle} added to {albumName}!";
        }
    }
}
