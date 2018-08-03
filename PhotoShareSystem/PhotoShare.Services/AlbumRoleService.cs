using System.Linq;

namespace PhotoShare.Services
{
    using System;

    using Models;
    using Models.Enums;
    using Data;
    using Contracts;

    public class AlbumRoleService : IAlbumRoleService
    {
        private const string InvalidRole = "Permission must be either “Owner” or “Viewer”!";
        private const string NoOwner = "This album does not have an owner!";
        private readonly PhotoShareContext context;

        public AlbumRoleService(PhotoShareContext context)
        {
            this.context = context;
        }

        public AlbumRole PublishAlbumRole(int albumId, int userId, string role)
        {

            Role roleAsEnum;

            try
            {
                roleAsEnum = Enum.Parse<Role>(role, true);
            }
            catch
            {
                throw new ArgumentException(InvalidRole);
            }

            var albumRole = new AlbumRole()
            {
                AlbumId = albumId,
                UserId = userId,
                Role = roleAsEnum
            };

           this.context.AlbumRoles.Add(albumRole);

            this.context.SaveChanges();

            return albumRole;
        }

        public int GetOwnerId(int albumId)
        {
            var ownerId = context.AlbumRoles.FirstOrDefault(p => p.AlbumId == albumId && p.Role == Role.Owner)?.UserId;

            if (ownerId == null)
            {
                throw new ArgumentException(NoOwner);
            }

            return ownerId.Value;
        }
    }
}
