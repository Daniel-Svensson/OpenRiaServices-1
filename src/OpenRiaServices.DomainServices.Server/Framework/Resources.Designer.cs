﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenRiaServices.DomainServices.Server {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("OpenRiaServices.DomainServices.Server.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;summary&gt;
        ///Gets a value indicating whether the identity is authenticated.
        ///&lt;/summary&gt;
        ///&lt;remarks&gt;
        ///This value is &lt;c&gt;true&lt;/c&gt; if &lt;see cref=&quot;Name&quot;/&gt; is not &lt;c&gt;null&lt;/c&gt; or empty.
        ///&lt;/remarks&gt;.
        /// </summary>
        internal static string ApplicationServices_CommentIsAuth {
            get {
                return ResourceManager.GetString("ApplicationServices_CommentIsAuth", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;summary&gt;
        ///Return whether the principal is in the role.
        ///&lt;/summary&gt;
        ///&lt;remarks&gt;
        ///Returns whether the specified role is contained in the roles.
        ///This implementation is case sensitive.
        ///&lt;/remarks&gt;
        ///&lt;param name=&quot;role&quot;&gt;The name of the role for which to check membership.&lt;/param&gt;
        ///&lt;returns&gt;Whether the principal is in the role.&lt;/returns&gt;.
        /// </summary>
        internal static string ApplicationServices_CommentIsInRole {
            get {
                return ResourceManager.GetString("ApplicationServices_CommentIsInRole", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The AuthenticationServiceAttribute can only be applied to DomainServices that implement the IAuthentication&lt;&gt; interface..
        /// </summary>
        internal static string ApplicationServices_MustBeIAuth {
            get {
                return ResourceManager.GetString("ApplicationServices_MustBeIAuth", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} type must not implement IAuthentication&lt;&gt; explicitly and each method must be included in the generated context..
        /// </summary>
        internal static string ApplicationServices_MustBeIAuthImpl {
            get {
                return ResourceManager.GetString("ApplicationServices_MustBeIAuthImpl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} type must not implement IUser explicitly..
        /// </summary>
        internal static string ApplicationServices_MustBeIUser {
            get {
                return ResourceManager.GetString("ApplicationServices_MustBeIUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} member in {1} must be included in the generated entity..
        /// </summary>
        internal static string ApplicationServices_MustBeSerializable {
            get {
                return ResourceManager.GetString("ApplicationServices_MustBeSerializable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Name property in {0} must be marked with the KeyAttribute..
        /// </summary>
        internal static string ApplicationServices_NameMustBeAKey {
            get {
                return ResourceManager.GetString("ApplicationServices_NameMustBeAKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There was a failure using the default &apos;{0}Provider&apos;. Please make sure it is configured correctly. {1}.
        /// </summary>
        internal static string ApplicationServices_ProviderError {
            get {
                return ResourceManager.GetString("ApplicationServices_ProviderError", resourceCulture);
            }
        }
    }
}