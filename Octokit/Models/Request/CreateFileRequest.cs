﻿using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit.Models.Request
{
    /// <summary>
    /// Base class with common properties for all the Repository Content Request APIs.
    /// </summary>
    /// 
    public abstract class ContentRequest
    {
        protected ContentRequest(string message)
        {
            Ensure.ArgumentNotNullOrEmptyString(message, "message");
            
            Message = message;
        }

        /// <summary>
        /// The commit message. This is required.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// The branch name. If null, this defaults to the default branch which is usually "master".
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// Specifies the committer to use for the commit. This is optional.
        /// </summary>
        public Signature Committer { get; set; }

        /// <summary>
        /// Specifies the author to use for the commit. This is optional.
        /// </summary>
        public Signature Author { get; set; }
    }

    /// <summary>
    /// Represents the request to delete a file in a repository.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DeleteFileRequest : ContentRequest
    {
        public DeleteFileRequest(string message, string sha) : base(message)
        {
            Ensure.ArgumentNotNullOrEmptyString(sha, "sha");

            Sha = sha;
        }
        public string Sha { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "SHA: {0} Message: {1}", Sha, Message);
            }
        }
    }

    /// <summary>
    /// Represents the parameters to create a file in a repository.
    /// </summary>
    /// <remarks>https://developer.github.com/v3/repos/contents/#create-a-file</remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CreateFileRequest : ContentRequest
    {
        /// <summary>
        /// Creates an instance of a <see cref="CreateFileRequest" />.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="content"></param>
        public CreateFileRequest(string message, string content) : base(message)
        {
            Ensure.ArgumentNotNull(content, "content");

            Content = content;
        }

        /// <summary>
        /// The contents of the file to create. This is required.
        /// </summary>
        public string Content { get; private set; }

        internal virtual string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Message: {0} Content: {1}", Message, Content);
            }
        }
    }

    /// <summary>
    /// Represents the parameters to update a file in a repository.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UpdateFileRequest : CreateFileRequest
    {
        public UpdateFileRequest(string message, string content, string sha)
            : base(message, content)
        {
            Ensure.ArgumentNotNullOrEmptyString(sha, "sha");

            Sha = sha;
        }

        /// <summary>
        /// The blob SHA of the file being replaced.
        /// </summary>
        public string Sha { get; private set; }

        internal override string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "SHA: {0} Message: {1}", Sha, Message);
            }
        }
    }
}
