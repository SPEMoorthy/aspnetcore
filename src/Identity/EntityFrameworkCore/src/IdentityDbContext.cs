// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.AspNetCore.Identity.EntityFrameworkCore
{
    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    public class IdentityDbContext : IdentityDbContext<IdentityUser, IdentityRole, int, string>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IdentityDbContext"/>.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public IdentityDbContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityDbContext" /> class.
        /// </summary>
        protected IdentityDbContext() { }
    }

    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    /// <typeparam name="TUser">The type of the user objects.</typeparam>
    public class IdentityDbContext<TUser> : IdentityDbContext<TUser, IdentityRole, int, string> where TUser : IdentityUser
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IdentityDbContext"/>.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public IdentityDbContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityDbContext" /> class.
        /// </summary>
        protected IdentityDbContext() { }
    }

    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    /// <typeparam name="TUser">The type of user objects.</typeparam>
    /// <typeparam name="TRole">The type of role objects.</typeparam>
    /// <typeparam name="TKeyCompId">The type of the primary key for users and roles.</typeparam>
    /// <typeparam name="TKeyId">The type of the primary key for users and roles.</typeparam>
    public class IdentityDbContext<TUser, TRole, TKeyCompId, TKeyId> : IdentityDbContext<TUser, TRole, TKeyCompId, TKeyId, IdentityUserClaim<TKeyCompId, TKeyId>, IdentityUserRole<TKeyCompId, TKeyId>, IdentityUserLogin<TKeyCompId, TKeyId>, IdentityRoleClaim<TKeyCompId, TKeyId>, IdentityUserToken<TKeyCompId, TKeyId>>
        where TUser : IdentityUser<TKeyCompId, TKeyId>
        where TRole : IdentityRole<TKeyCompId, TKeyId>
        where TKeyCompId : IEquatable<TKeyCompId>
        where TKeyId : IEquatable<TKeyId>
    {
        /// <summary>
        /// Initializes a new instance of the db context.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public IdentityDbContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        protected IdentityDbContext() { }
    }

    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    /// <typeparam name="TUser">The type of user objects.</typeparam>
    /// <typeparam name="TRole">The type of role objects.</typeparam>
    /// <typeparam name="TKeyCompId">The type of the primary key for users and roles.</typeparam>
    /// <typeparam name="TKeyId">The type of the primary key for users and roles.</typeparam>
    /// <typeparam name="TUserClaim">The type of the user claim object.</typeparam>
    /// <typeparam name="TUserRole">The type of the user role object.</typeparam>
    /// <typeparam name="TUserLogin">The type of the user login object.</typeparam>
    /// <typeparam name="TRoleClaim">The type of the role claim object.</typeparam>
    /// <typeparam name="TUserToken">The type of the user token object.</typeparam>
    public abstract class IdentityDbContext<TUser, TRole, TKeyCompId, TKeyId, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken> : IdentityUserContext<TUser, TKeyCompId, TKeyId, TUserClaim, TUserLogin, TUserToken>
        where TUser : IdentityUser<TKeyCompId, TKeyId>
        where TRole : IdentityRole<TKeyCompId, TKeyId>
        where TKeyCompId : IEquatable<TKeyCompId>
        where TKeyId : IEquatable<TKeyId>
        where TUserClaim : IdentityUserClaim<TKeyCompId, TKeyId>
        where TUserRole : IdentityUserRole<TKeyCompId, TKeyId>
        where TUserLogin : IdentityUserLogin<TKeyCompId, TKeyId>
        where TRoleClaim : IdentityRoleClaim<TKeyCompId, TKeyId>
        where TUserToken : IdentityUserToken<TKeyCompId, TKeyId>
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public IdentityDbContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        protected IdentityDbContext() { }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of User roles.
        /// </summary>
        public virtual DbSet<TUserRole> UserRoles { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of roles.
        /// </summary>
        public virtual DbSet<TRole> Roles { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of role claims.
        /// </summary>
        public virtual DbSet<TRoleClaim> RoleClaims { get; set; }

        /// <summary>
        /// Configures the schema needed for the identity framework.
        /// </summary>
        /// <param name="builder">
        /// The builder being used to construct the model for this context.
        /// </param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TUser>(b =>
            {
                b.HasMany<TUserRole>().WithOne().HasForeignKey(ur => new { ur.RoleCompId, ur.UserId } ).IsRequired();
            });

            builder.Entity<TRole>(b =>
            {
                b.HasKey(r => new { r.CompId, r.Id } );
                b.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();
                b.ToTable("AspNetRoles");
                b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

                b.Property(u => u.Name).HasMaxLength(256);
                b.Property(u => u.NormalizedName).HasMaxLength(256);

                b.HasMany<TUserRole>().WithOne().HasForeignKey(ur =>  new { ur.RoleCompId, ur.RoleId }).IsRequired();
                b.HasMany<TRoleClaim>().WithOne().HasForeignKey(rc => new { rc.RoleCompId, rc.RoleId }).IsRequired();
            });

            builder.Entity<TRoleClaim>(b =>
            {
                b.HasKey(rc => new { rc.RoleCompId, rc.Id });
                b.ToTable("AspNetRoleClaims");
            });

            builder.Entity<TUserRole>(b =>
            {
                b.HasKey(r => new { r.UserCompId, r.UserId, r.RoleCompId,  r.RoleId });
                b.ToTable("AspNetUserRoles");
            });
        }
    }
}
