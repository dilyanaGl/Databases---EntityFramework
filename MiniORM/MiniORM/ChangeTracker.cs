using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace MiniORM
{
    internal class ChangeTracker<T> where T: class, new()
    {
        private readonly List<T> allEntities;

        private readonly List<T> added;

        private readonly List<T> removed;

        public ChangeTracker(IEnumerable<T> еntities)
        {
            this.allEntities = CloneEntities(еntities);
            this.added = new List<T>();
            this.removed = new List<T>();
        }

        public IReadOnlyCollection<T> AllEntities => this.allEntities.AsReadOnly();

        public IReadOnlyCollection<T> Added => this.added.AsReadOnly();

        public IReadOnlyCollection<T> Removed => this.removed.AsReadOnly();

        private List<T> CloneEntities(IEnumerable<T> entities)
        {
            var clonedEntities = new List<T>();

            var propertiesToClone = typeof(T).GetProperties()
                .Where(p => DbContext.AllowedSqlTypes.Contains(p.PropertyType));

            foreach (var item in entities)
            {
                var clonedEntity = Activator.CreateInstance<T>();
            
                foreach (var prop in propertiesToClone)
                {
                    var value = prop.GetValue(item);
                    prop.SetValue(clonedEntity, value);
                }

                clonedEntities.Add(clonedEntity);
             }

            return clonedEntities;

        }

        public void Add(T item) => this.added.Add(item);

        public void Remove(T item) => this.removed.Add(item);

        public IEnumerable<T> GetModifiedEntities(DbSet<T> dbSet)
        {
            var modifiedEntitites = new List<T>();

            var primaryKeys = typeof(T).GetProperties()
                .Where(p => p.HasAttribute<KeyAttribute>())
                .ToArray();

            foreach (var proxyEntity in this.AllEntities)
            {
                var primaryKeyValues = GetPrimaryKeyValues(primaryKeys, proxyEntity).ToArray();

                var entity = dbSet.Entities.Single(e =>
                    GetPrimaryKeyValues(primaryKeys, e).SequenceEqual(primaryKeyValues));

                bool isModified = IsModified(proxyEntity, entity);

                if(isModified)
                {
                    modifiedEntitites.Add(entity);
                }
            }

            return modifiedEntitites;
        }

        private static bool IsModified(T proxyEntity, T entity)
        {
            var monitoredProperties = typeof(T).GetProperties()
                .Where(p => DbContext.AllowedSqlTypes.Contains(p.PropertyType));

            var modifiedProperties = monitoredProperties
                .Where(p => !Equals(p.GetValue(entity), p.GetValue(proxyEntity))).ToArray();

            var isModified = modifiedProperties.Any();

            return isModified;
        }


        private static IEnumerable<object> GetPrimaryKeyValues(PropertyInfo[] primaryKeys, T proxyEntity)
        {
            return primaryKeys.Select(p => p.GetValue(proxyEntity));
        }
    }
}