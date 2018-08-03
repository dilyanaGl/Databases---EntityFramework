using System;
using System.Linq;
using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Services
{
    public class AlbumTagService : IAlbumTagService
    {
        
        private readonly PhotoShareContext context;

        public AlbumTagService(PhotoShareContext context)
        {
             this.context = context;
        }


        public AlbumTag AddTagTo(int albumId, int tagId)
        {
            var album = context.Albums.SingleOrDefault(p => p.Id == albumId);
            var tag = context.Tags.SingleOrDefault(p => p.Id == tagId);

            if (album == null || tag == null)
            {
                throw new ArgumentException($"Invalid album or tag id!");
            }

            var albumTag = new AlbumTag()
            {
                Album = album, 
                AlbumId = albumId, 
                Tag = tag, 
                TagId = tagId
            };

            context.Add(albumTag);
            context.SaveChanges();

            return albumTag;
        }
    }
}
