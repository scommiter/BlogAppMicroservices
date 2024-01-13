﻿using Contracts.Domains.Interfaces;

namespace Contracts.Domains
{
    public abstract class EntityAuditBase<T> : EntityBase<T>, IAuditable
    {
        private DateTimeOffset _createdDate = DateTimeOffset.UtcNow;

        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set => _createdDate = (value == default) ? DateTimeOffset.UtcNow : value;
        }

        public DateTimeOffset? LastModifiedDate { get; set; }
    }
}
 