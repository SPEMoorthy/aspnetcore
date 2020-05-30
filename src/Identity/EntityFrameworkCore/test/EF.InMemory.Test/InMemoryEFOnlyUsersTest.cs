// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity.Test;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Microsoft.AspNetCore.Identity.EntityFrameworkCore.InMemory.Test
{
    public class InMemoryEFOnlyUsersTest
        : UserManagerSpecificationTestBase<IdentityUser, string>,
            IClassFixture<InMemoryDatabaseFixture>
    {
        private readonly InMemoryDatabaseFixture _fixture;

        public InMemoryEFOnlyUsersTest(InMemoryDatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        protected override object CreateTestContext()
            => InMemoryContext<IdentityUser>.Create(_fixture.Connection);

        protected override void AddUserStore(IServiceCollection services, object context = null)
            => services.AddSingleton<IUserStore<IdentityUser>>(new UserStore<IdentityUser, IdentityRole, DbContext, int, string, IdentityUserClaim<int, string>, IdentityUserRole<int, string>, IdentityUserLogin<int, string>, IdentityUserToken<int, string>, IdentityRoleClaim<int, string>>((InMemoryContext<IdentityUser>)context, new IdentityErrorDescriber()));

        protected override IdentityUser CreateTestUser(string namePrefix = "", string email = "", string phoneNumber = "",
            bool lockoutEnabled = false, DateTimeOffset? lockoutEnd = default(DateTimeOffset?), bool useNamePrefixAsUserName = false)
        {
            return new IdentityUser
            {
                UserName = useNamePrefixAsUserName ? namePrefix : string.Format("{0}{1}", namePrefix, Guid.NewGuid()),
                Email = email,
                PhoneNumber = phoneNumber,
                LockoutEnabled = lockoutEnabled,
                LockoutEnd = lockoutEnd
            };
        }

        protected override void SetUserPasswordHash(IdentityUser user, string hashedPassword)
        {
            user.PasswordHash = hashedPassword;
        }

        protected override Expression<Func<IdentityUser, bool>> UserNameEqualsPredicate(string userName) => u => u.UserName == userName;

        protected override Expression<Func<IdentityUser, bool>> UserNameStartsWithPredicate(string userName) => u => u.UserName.StartsWith(userName);
    }
}
