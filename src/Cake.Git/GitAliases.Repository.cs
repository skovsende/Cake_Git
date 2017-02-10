﻿using System;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;
using Cake.Git.Extensions;
using LibGit2Sharp;
// ReSharper disable UnusedMember.Global

namespace Cake.Git
{
    public static partial class GitAliases
    { 
        /// <summary>
        /// Checks if a specific directory is a valid Git repository.
        /// </summary>
        /// <example>
        /// <code>
        ///     var result = GitIsValidRepository("c:/temp/cake");
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="path">Path to the repository to check.</param>
        /// <returns>True if the path is part of a valid Git Repository.</returns>
        /// <exception cref="ArgumentNullException">If any of the parameters are null.</exception>
        /// <exception cref="RepositoryNotFoundException">If path doesn't exist.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Repository")]
        public static bool GitIsValidRepository(this ICakeContext context, DirectoryPath path)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!context.FileSystem.Exist(path))
            {
                throw new RepositoryNotFoundException($"Path '{path}' doesn't exists.");
            }

            return Repository.IsValid(path.FullPath);
        }

        /// <summary>
        /// Checks if a repository contains uncommited changes.
        /// </summary>
        /// <example>
        /// <code>
        ///     var result = GitHasUncommitedChanges("c:/temp/cake");
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="path">Path to the repository to check.</param>
        /// <returns>True if the Git repository contains uncommited changes.</returns>
        /// <exception cref="ArgumentNullException">If any of the parameters are null.</exception>
        /// <exception cref="RepositoryNotFoundException">If path doesn't exist.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Repository")]
        public static bool GitHasUncommitedChanges(this ICakeContext context, DirectoryPath path)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!context.FileSystem.Exist(path))
            {
                throw new RepositoryNotFoundException($"Path '{path}' doesn't exists.");
            }

            return context.UseRepository(
                path,
                repository => repository.RetrieveStatus().IsDirty);
        }
    }
}