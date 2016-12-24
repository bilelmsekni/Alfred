using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Alfred.Configuration
{
    internal static class Resources
    {
        private static readonly ResourceManager ResourceManager = new ResourceManager("Resources", typeof(Resources).GetTypeInfo().Assembly);

        /// <summary>
        /// Cannot create instance of type '{0}' because it is either abstract or an interface.
        /// </summary>
        internal static string ErrorCannotActivateAbstractOrInterface => GetString("Error_CannotActivateAbstractOrInterface");

        /// <summary>Failed to convert '{0}' to type '{1}'.</summary>
        internal static string ErrorFailedBinding => GetString("Error_FailedBinding");

        /// <summary>Failed to create instance of type '{0}'.</summary>
        internal static string ErrorFailedToActivate => GetString("Error_FailedToActivate");

        /// <summary>
        /// Cannot create instance of type '{0}' because it is missing a public parameterless constructor.
        /// </summary>
        internal static string ErrorMissingParameterlessConstructor => GetString("Error_MissingParameterlessConstructor");

        /// <summary>
        /// Cannot create instance of type '{0}' because multidimensional arrays are not supported.
        /// </summary>
        internal static string ErrorUnsupportedMultidimensionalArray => GetString("Error_UnsupportedMultidimensionalArray");

        /// <summary>
        /// Cannot create instance of type '{0}' because it is either abstract or an interface.
        /// </summary>
        internal static string FormatError_CannotActivateAbstractOrInterface(object p0)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("Error_CannotActivateAbstractOrInterface"), new object[1] { p0 });
        }

        /// <summary>Failed to convert '{0}' to type '{1}'.</summary>
        internal static string FormatError_FailedBinding(object p0, object p1)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("Error_FailedBinding"), new object[2] { p0, p1 });
        }

        /// <summary>Failed to create instance of type '{0}'.</summary>
        internal static string FormatError_FailedToActivate(object p0)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("Error_FailedToActivate"), new object[1] { p0 });
        }

        /// <summary>
        /// Cannot create instance of type '{0}' because it is missing a public parameterless constructor.
        /// </summary>
        internal static string FormatError_MissingParameterlessConstructor(object p0)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("Error_MissingParameterlessConstructor"), new object[1] { p0 });
        }

        /// <summary>
        /// Cannot create instance of type '{0}' because multidimensional arrays are not supported.
        /// </summary>
        internal static string FormatError_UnsupportedMultidimensionalArray(object p0)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("Error_UnsupportedMultidimensionalArray"), new object[1] { p0 });
        }

        private static string GetString(string name, params string[] formatterNames)
        {
            string str = ResourceManager.GetString(name);
            if (formatterNames != null)
            {
                for (int index = 0; index < formatterNames.Length; ++index)
                    str = str.Replace("{" + formatterNames[index] + "}", "{" + index + "}");
            }
            return str;
        }
    }
}