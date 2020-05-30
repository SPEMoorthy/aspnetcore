// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.AspNetCore.Identity.EntityFrameworkCore.InMemory.Test
{
    public class InMemoryContext :
        InMemoryContext<IdentityUser, IdentityRole, int, string>
    {
        private InMemoryContext(DbConnection connection) : base(connection)
        { }

        public new static InMemoryContext Create(DbConnection connection)
            => Initialize(new InMemoryContext(connection));

        public static TContext Initialize<TContext>(TContext context) where TContext : DbContext
        {
            context.Database.EnsureCreated();

            return context;
        }
    }

    public class InMemoryContext<TUser> : IdentityUserContext<TUser, int, string>
        where TUser : IdentityUser
    {
        private readonly DbConnection _connection;

        private InMemoryContext(DbConnection connection)
        {
            _connection = connection;
        }

        public static InMemoryContext<TUser> Create(DbConnection connection)
            => InMemoryContext.Initialize(new InMemoryContext<TUser>(connection));

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite(_connection);
    }

    public class InMemoryContext<TUser, TRole, TKeyCompId, TKeyId> : IdentityDbContext<TUser, TRole, TKeyCompId, TKeyId>
        where TUser : IdentityUser<TKeyCompId, TKeyId>
        where TRole : IdentityRole<TKeyCompId, TKeyId>
        where TKeyCompId : IEquatable<TKeyCompId>
        where TKeyId : IEquatable<TKeyId>
    {
        private readonly DbConnection _connection;

        protected InMemoryContext(DbConnection connection)
        {
            _connection = connection;
        }

        public static InMemoryContext<TUser, TRole, TKeyCompId, TKeyId> Create(DbConnection connection)
            => InMemoryContext.Initialize(new InMemoryContext<TUser, TRole, TKeyCompId, TKeyId>(connection));

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite(_connection);
    }

    public abstract class InMemoryContext<TUser, TRole, TKeyCompId, TKeyId, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken> :
            IdentityDbContext<TUser, TRole, TKeyCompId, TKeyId, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>
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
        protected InMemoryContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
