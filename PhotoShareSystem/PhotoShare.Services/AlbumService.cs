using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Models.Enums;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly PhotoShareContext context;


        private const string ColorNotFound = "Color {0} not found!";
        private const string InvalidTags = "Invalid tags!";

        public AlbumService(PhotoShareContext context)
        {
            this.context = context;

        }

        public TModel ById<TModel>(int id)
            => By<TModel>(p => p.Id == id).SingleOrDefault();

        public TModel ByName<TModel>(string name)
            => By<TModel>(p => p.Name == name).SingleOrDefault();

        public bool Exists(int id)
            => ById<Album>(id) != null;

        public bool Exists(string name)
            => ByName<Album>(name) != null;

        public Album Create(int userId, string albumTitle, string BgColor, string[] tags)
        {
            Color color;
            try
            {
                color = Enum.Parse<Color>(BgColor);

            }
            catch
            {
                throw new ArgumentException(String.Format(ColorNotFound, BgColor));
            }

            var album = new Album
            {
                Name = albumTitle,
                BackgroundColor = color,
                IsPublic = true
            };


            context.Add(album);
            //context.SaveChanges();

            var user = context.Users.SingleOrDefault(p => p.Id == userId);

            var albumRole = new AlbumRole()
            {
                AlbumId = album.Id,
                User = user,
                Role = Role.Owner
            };

            context.AlbumRoles.Add(albumRole);

            var albumTags = new List<AlbumTag>();

            foreach (var t in tags)
            {
                var tag = context.Tags.FirstOrDefault(p => p.Name == t);


                if (tag == null)
                {
                    throw new ArgumentException(InvalidTags);
                }

                

                var albumTag = new AlbumTag()
                {
                    AlbumId = album.Id,
                    Tag = tag
                };

                albumTags.Add(albumTag);
            }

            context.AlbumTags.AddRange(albumTags);
            context.SaveChanges();

            return album;
        }

        private IEnumerable<TModel> By<TModel>(Func<Album, bool> predicate)
            => this.context
                .Albums
                .Where(predicate)
                .AsQueryable()
                .ProjectTo<TModel>();
    }
}

