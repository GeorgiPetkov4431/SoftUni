using System.Collections;

namespace MiniORM
{
    public class DbSet<TEntity> : ICollection<TEntity>
        where TEntity : class, new()
    {
        internal DbSet(IEnumerable<TEntity> entities)
        {
            this.Entities = entities.ToList();
            this.ChangeTracker = new ChangeTracker<TEntity>(entities);
        }

        internal IList<TEntity> Entities { get; }

        internal ChangeTracker<TEntity> ChangeTracker { get; }

        public int Count
            => this.Entities.Count();

        public bool IsReadOnly
            => this.Entities.IsReadOnly;

        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException(nameof(entity), "Added entity cannot be null");
            }

            /* Add the new entity to the in-memory DbSet Entity collection to enable the application to access it before persisting */
            this.Entities.Add(entity);

            /* Add the new entity to the ChangeTracker to enable the application to persist it later */
            /* Notify the ChangeTracker! */
            this.ChangeTracker.Add(entity);
        }

        public bool Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException(nameof(entity), "Removed entity cannot be null");
            }

            /* Try to remove the Entity from the local in-memory DbSet Entity collection to notify application for deleting! */
            bool removedSuccessfully = this.Entities.Remove(entity);
            if (removedSuccessfully)
            {
                /* Notify the ChangeTracker to mark the Entity as deleted for persistence! */
                this.ChangeTracker.Remove(entity);
            }

            return removedSuccessfully;
        }

        public bool RemoveRange(IEnumerable<TEntity> entities)
        {
            bool result = true;
            if (entities == null)
            {
                throw new ArgumentException(nameof(entities), "Entity collection to remove cannot be null");
            }

            foreach (TEntity entity in entities)
            {
                result &= this.Remove(entity);
            }

            return result;
        }

        public void Clear()
        {
            while (this.Count > 0)
            {
                TEntity entityToRemove = this.Entities.First();
                this.Remove(entityToRemove);
            }
        }

        public bool Contains(TEntity item)
        {
            return this.Entities.Contains(item);
        }

        public void CopyTo(TEntity[] array, int arrayIndex)
        {
            this.Entities.CopyTo(array, arrayIndex);
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return this.Entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
