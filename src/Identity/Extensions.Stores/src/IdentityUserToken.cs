// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.AspNetCore.Identity
{
    /// <summary>
    /// Represents an authentication token for a user.
    /// </summary>
    /// <typeparam name="TKeyCompId">The type of the primary key used for users.</typeparam>
    ///  <typeparam name="TKeyId">The type of the primary key used for users.</typeparam>
    public class IdentityUserToken<TKeyCompId, TKeyId> where TKeyCompId : IEquatable<TKeyCompId> where TKeyId : IEquatable<TKeyId>
    {

        /// <summary>
        /// Gets or sets the primary key of the user that the token belongs to.
        /// </summary>
        public virtual TKeyCompId UserCompId { get; set; }

        /// <summary>
        /// Gets or sets the primary key of the user that the token belongs to.
        /// </summary>
        public virtual TKeyId UserId { get; set; }

        /// <summary>
        /// Gets or sets the LoginProvider this token is from.
        /// </summary>
        public virtual string LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the name of the token.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the token value.
        /// </summary>
        [ProtectedPersonalData]
        public virtual string Value { get; set; }
    }
}
