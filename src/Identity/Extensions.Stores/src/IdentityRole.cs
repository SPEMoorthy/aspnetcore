// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.AspNetCore.Identity
{
    /// <summary>
    /// The default implementation of <see cref="IdentityRole{TKeyCompId, TKeyId}"/> which uses a string as the primary key.
    /// </summary>
    public class IdentityRole : IdentityRole<int, string>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IdentityRole"/>.
        /// </summary>
        /// <remarks>
        /// The Id property is initialized to form a new GUID string value.
        /// </remarks>
        public IdentityRole()
        {
            CompId = 1;
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="IdentityRole"/>.
        /// </summary>
        /// <param name="roleName">The role name.</param>
        /// <remarks>
        /// The Id property is initialized to form a new GUID string value.
        /// </remarks>
        public IdentityRole(string roleName) : this()
        {
            Name = roleName;
        }
    }

    /// <summary>
    /// Represents a role in the identity system
    /// </summary>
    /// <typeparam name="TKeyCompId">The type used for the primary key for the role.</typeparam>
    /// <typeparam name="TKeyId">The type used for the primary key for the role.</typeparam>
    public class IdentityRole<TKeyCompId, TKeyId> where TKeyCompId : IEquatable<TKeyCompId> where TKeyId : IEquatable<TKeyId>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IdentityRole{TKeyCompId, TKeyId}"/>.
        /// </summary>
        public IdentityRole() { }

        /// <summary>
        /// Initializes a new instance of <see cref="IdentityRole{TKeyCompId, TKeyId}"/>.
        /// </summary>
        /// <param name="roleName">The role name.</param>
        public IdentityRole( string roleName) : this()
        {
            Name = roleName;
        }

        /// <summary>
        /// Gets or sets the primary key for this role.
        /// </summary>
        public virtual TKeyCompId CompId { get; set; }


        /// <summary>
        /// Gets or sets the primary key for this role.
        /// </summary>
        public virtual TKeyId Id { get; set; }

       
        /// <summary>
        /// Gets or sets the name for this role.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the normalized name for this role.
        /// </summary>
        public virtual string NormalizedName { get; set; }

        /// <summary>
        /// A random value that should change whenever a role is persisted to the store
        /// </summary>
        public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Returns the name of the role.
        /// </summary>
        /// <returns>The name of the role.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
