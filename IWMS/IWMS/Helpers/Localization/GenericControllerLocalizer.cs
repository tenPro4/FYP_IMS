using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace IWMS.Helpers.Localization
{
    public class GenericControllerLocalizer<TResourceSource> : IGenericControllerLocalizer<TResourceSource>
    {
        private IStringLocalizer _localizer;

        public GenericControllerLocalizer(IStringLocalizerFactory factory)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));

            var type = typeof(TResourceSource);
            var assemblyName = type.GetTypeInfo().Assembly.GetName().Name;
            var baseName = (type.Namespace + "." + type.Name).Substring(assemblyName.Length).Trim('.');

            _localizer = factory.Create(baseName, assemblyName);
        }

        /// <inheritdoc />
        public virtual IStringLocalizer WithCulture(CultureInfo culture)
        {
            return _localizer.WithCulture(culture);
        }

        /// <inheritdoc />
        public virtual LocalizedString this[string name]
        {
            get
            {
                if (name == null)
                    throw new ArgumentNullException(nameof(name));
                return _localizer[name];
            }
        }

        /// <inheritdoc />
        public virtual LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                if (name == null)
                    throw new ArgumentNullException(nameof(name));
                return _localizer[name, arguments];
            }
        }

        /// <inheritdoc />
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return _localizer.GetAllStrings(includeParentCultures);
        }
    }
}
