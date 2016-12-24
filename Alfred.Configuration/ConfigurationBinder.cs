using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Alfred.Configuration
{
    internal static class ConfigurationBinder
    {
        /// <summary>
        /// Attempts to bind the configuration instance to a new instance of type T.
        /// If this configuration section has a value, that will be used.
        /// Otherwise binding by matching property names against configuration keys recursively.
        /// </summary>
        /// <typeparam name="T">The type of the new instance to bind.</typeparam>
        /// <param name="configuration">The configuration instance to bind.</param>
        /// <returns>The new instance of T if successful, default(T) otherwise.</returns>
        internal static T Get<T>(this IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            object obj = configuration.Get(typeof(T));
            if (obj == null)
                return default(T);
            return (T)obj;
        }

        /// <summary>
        /// Attempts to bind the configuration instance to a new instance of type T.
        /// If this configuration section has a value, that will be used.
        /// Otherwise binding by matching property names against configuration keys recursively.
        /// </summary>
        /// <param name="configuration">The configuration instance to bind.</param>
        /// <param name="type">The type of the new instance to bind.</param>
        /// <returns>The new instance if successful, null otherwise.</returns>
        internal static object Get(this IConfiguration configuration, Type type)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            return BindInstance(type, null, configuration);
        }

        /// <summary>
        /// Attempts to bind the given object instance to configuration values by matching property names against configuration keys recursively.
        /// </summary>
        /// <param name="configuration">The configuration instance to bind.</param>
        /// <param name="instance">The object to bind.</param>
        internal static void Bind(this IConfiguration configuration, object instance)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            if (instance == null)
                return;
            BindInstance(instance.GetType(), instance, configuration);
        }

        /// <summary>
        /// Extracts the value with the specified key and converts it to type T.
        /// </summary>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="key">The configuration key for the value to convert.</param>
        /// <returns>The converted value.</returns>
        internal static T GetValue<T>(this IConfiguration configuration, string key)
        {
            return configuration.GetValue(key, default(T));
        }

        /// <summary>
        /// Extracts the value with the specified key and converts it to type T.
        /// </summary>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="key">The configuration key for the value to convert.</param>
        /// <param name="defaultValue">The default value to use if no value is found.</param>
        /// <returns>The converted value.</returns>
        internal static T GetValue<T>(this IConfiguration configuration, string key, T defaultValue)
        {
            return (T)configuration.GetValue(typeof(T), key, defaultValue);
        }

        /// <summary>
        /// Extracts the value with the specified key and converts it to the specified type.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="type">The type to convert the value to.</param>
        /// <param name="key">The configuration key for the value to convert.</param>
        /// <returns>The converted value.</returns>
        internal static object GetValue(this IConfiguration configuration, Type type, string key)
        {
            return configuration.GetValue(type, key, null);
        }

        /// <summary>
        /// Extracts the value with the specified key and converts it to the specified type.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="type">The type to convert the value to.</param>
        /// <param name="key">The configuration key for the value to convert.</param>
        /// <param name="defaultValue">The default value to use if no value is found.</param>
        /// <returns>The converted value.</returns>
        internal static object GetValue(this IConfiguration configuration, Type type, string key, object defaultValue)
        {
            string str = configuration.GetSection(key).Value;
            if (str != null)
                return ConvertValue(type, str);
            return defaultValue;
        }

        private static void BindNonScalar(this IConfiguration configuration, object instance)
        {
            if (instance == null)
                return;
            foreach (PropertyInfo allProperty in GetAllProperties(instance.GetType().GetTypeInfo()))
                BindProperty(allProperty, instance, configuration);
        }

        private static void BindProperty(PropertyInfo property, object instance, IConfiguration config)
        {
            if (property.GetMethod == null || !property.GetMethod.IsPublic || property.GetMethod.GetParameters().Length != 0)
                return;
            object instance1 = property.GetValue(instance);
            bool flag = property.SetMethod != null && property.SetMethod.IsPublic;
            if (instance1 == null && !flag)
                return;
            object obj = BindInstance(property.PropertyType, instance1, config.GetSection(property.Name));
            if (!(obj != null & flag))
                return;
            property.SetValue(instance, obj);
        }

        private static object BindToCollection(TypeInfo typeInfo, IConfiguration config)
        {
            Type type = typeof(List<>).MakeGenericType(typeInfo.GenericTypeArguments[0]);
            object instance = Activator.CreateInstance(type);
            Type collectionType = type;
            IConfiguration config1 = config;
            BindCollection(instance, collectionType, config1);
            return instance;
        }

        private static object AttemptBindToCollectionInterfaces(Type type, IConfiguration config)
        {
            TypeInfo typeInfo = type.GetTypeInfo();
            if (!typeInfo.IsInterface)
                return null;
            if (FindOpenGenericInterface(typeof(IReadOnlyList<>), type) != null)
                return BindToCollection(typeInfo, config);
            if (FindOpenGenericInterface(typeof(IReadOnlyDictionary<,>), type) != null)
            {
                Type type1 = typeof(Dictionary<,>).MakeGenericType(typeInfo.GenericTypeArguments[0], typeInfo.GenericTypeArguments[1]);
                object instance = Activator.CreateInstance(type1);
                Type dictionaryType = type1;
                IConfiguration config1 = config;
                BindDictionary(instance, dictionaryType, config1);
                return instance;
            }
            Type genericInterface = FindOpenGenericInterface(typeof(IDictionary<,>), type);
            if (genericInterface != null)
            {
                object instance = Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(typeInfo.GenericTypeArguments[0], typeInfo.GenericTypeArguments[1]));
                Type dictionaryType = genericInterface;
                IConfiguration config1 = config;
                BindDictionary(instance, dictionaryType, config1);
                return instance;
            }
            if (FindOpenGenericInterface(typeof(IReadOnlyCollection<>), type) != null || FindOpenGenericInterface(typeof(ICollection<>), type) != null || FindOpenGenericInterface(typeof(IEnumerable<>), type) != null)
                return BindToCollection(typeInfo, config);
            return null;
        }

        private static object BindInstance(Type type, object instance, IConfiguration config)
        {
            if (type == typeof(IConfigurationSection))
                return config;
            IConfigurationSection configurationSection = config as IConfigurationSection;
            string str = configurationSection?.Value;
            if (str != null)
                return ConvertValue(type, str);
            if (config != null && config.GetChildren().Any())
            {
                if (instance == null)
                {
                    instance = AttemptBindToCollectionInterfaces(type, config);
                    if (instance != null)
                        return instance;
                    instance = CreateInstance(type);
                }
                Type genericInterface1 = FindOpenGenericInterface(typeof(IDictionary<,>), type);
                if (genericInterface1 != null)
                    BindDictionary(instance, genericInterface1, config);
                else if (type.IsArray)
                {
                    instance = BindArray((Array)instance, config);
                }
                else
                {
                    Type genericInterface2 = FindOpenGenericInterface(typeof(ICollection<>), type);
                    if (genericInterface2 != null)
                        BindCollection(instance, genericInterface2, config);
                    else
                        config.BindNonScalar(instance);
                }
            }
            return instance;
        }

        private static object CreateInstance(Type type)
        {
            TypeInfo typeInfo = type.GetTypeInfo();
            if (typeInfo.IsInterface || typeInfo.IsAbstract)
                throw new InvalidOperationException(Resources.FormatError_CannotActivateAbstractOrInterface(type));
            if (type.IsArray)
            {
                if (typeInfo.GetArrayRank() > 1)
                    throw new InvalidOperationException(Resources.FormatError_UnsupportedMultidimensionalArray(type));
                return Array.CreateInstance(typeInfo.GetElementType(), new int[1]);
            }
            IEnumerable<ConstructorInfo> declaredConstructors = typeInfo.DeclaredConstructors;
            Func<ConstructorInfo, bool> func = ctor =>
            {
                if (ctor.IsPublic)
                    return ctor.GetParameters().Length == 0;
                return false;
            };
            if (!declaredConstructors.Any<ConstructorInfo>(func))
                throw new InvalidOperationException(Resources.FormatError_MissingParameterlessConstructor((object)type));
            try
            {
                return Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(Resources.FormatError_FailedToActivate(type), ex);
            }
        }

        private static void BindDictionary(object dictionary, Type dictionaryType, IConfiguration config)
        {
            TypeInfo typeInfo = dictionaryType.GetTypeInfo();
            Type type1 = typeInfo.GenericTypeArguments[0];
            Type type2 = typeInfo.GenericTypeArguments[1];
            if (type1 != typeof(string))
                return;
            MethodInfo declaredMethod = typeInfo.GetDeclaredMethod("Add");
            foreach (IConfigurationSection child in config.GetChildren())
            {
                object obj = BindInstance(type2, null, child);
                if (obj != null)
                {
                    string key = child.Key;
                    declaredMethod.Invoke(dictionary, new object[2]
                    {
            key,
            obj
                    });
                }
            }
        }

        private static void BindCollection(object collection, Type collectionType, IConfiguration config)
        {
            TypeInfo typeInfo = collectionType.GetTypeInfo();
            Type type = typeInfo.GenericTypeArguments[0];
            string name = "Add";
            MethodInfo declaredMethod = typeInfo.GetDeclaredMethod(name);
            foreach (IConfigurationSection child in config.GetChildren())
            {
                try
                {
                    object obj = BindInstance(type, null, child);
                    if (obj != null)
                        declaredMethod.Invoke(collection, new object[1] { obj });
                }
                catch
                {
                }
            }
        }

        private static Array BindArray(Array source, IConfiguration config)
        {
            IConfigurationSection[] array = config.GetChildren().ToArray();
            int length = source.Length;
            Type elementType = source.GetType().GetElementType();
            Array instance = Array.CreateInstance(elementType, new int[1]
            {
        length + array.Length
            });
            if (length > 0)
                Array.Copy(source, instance, length);
            for (int index = 0; index < array.Length; ++index)
            {
                try
                {
                    object obj = BindInstance(elementType, null, array[index]);
                    if (obj != null)
                        instance.SetValue(obj, new int[1] { length + index });
                }
                catch
                {
                }
            }
            return instance;
        }

        private static object ConvertValue(Type type, string value)
        {
            if (type == typeof(object))
                return value;
            if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                if (string.IsNullOrEmpty(value))
                    return null;
                return ConvertValue(Nullable.GetUnderlyingType(type), value);
            }
            try
            {
                return TypeDescriptor.GetConverter(type).ConvertFromInvariantString(value);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(Resources.FormatError_FailedBinding(value, type), ex);
            }
        }

        private static Type FindOpenGenericInterface(Type expected, Type actual)
        {
            TypeInfo typeInfo = actual.GetTypeInfo();
            if (typeInfo.IsGenericType && actual.GetGenericTypeDefinition() == expected)
                return actual;
            foreach (Type implementedInterface in typeInfo.ImplementedInterfaces)
            {
                if (implementedInterface.GetTypeInfo().IsGenericType && implementedInterface.GetGenericTypeDefinition() == expected)
                    return implementedInterface;
            }
            return null;
        }

        private static IEnumerable<PropertyInfo> GetAllProperties(TypeInfo type)
        {
            List<PropertyInfo> propertyInfoList = new List<PropertyInfo>();
            do
            {
                propertyInfoList.AddRange(type.DeclaredProperties);
                type = type.BaseType.GetTypeInfo();
            }
            while (type != typeof(object).GetTypeInfo());
            return propertyInfoList;
        }
    }
}
