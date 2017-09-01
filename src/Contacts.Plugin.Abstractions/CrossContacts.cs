using Plugin.Contacts.Abstractions;
using System;

namespace Plugin.Contacts
{
    /// <summary>
    /// Cross platform Contacts implemenations
    /// </summary>
    public class CrossContacts
    {
        private static Type _type;

        public static void SetImplementation<TContactsImpl>() where TContactsImpl : IContacts
        {
            _type = typeof(TContactsImpl);
        }

        static Lazy<IContacts> Implementation = new Lazy<IContacts>(() => (IContacts)Activator.CreateInstance(_type), System.Threading.LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Current settings to use
        /// </summary>
        public static IContacts Current
        {
            get
            {
                var ret = Implementation.Value;
                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }
                return ret;
            }
        }

        internal static Exception NotImplementedInReferenceAssembly()
        {
            return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
        }
    }
}
