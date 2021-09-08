using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ActionApi
{
    public static class ReflectionExtensions
    {
        public static IEnumerable<Assembly> GetProjectLoadedAssemblies(
            string projectNamespacePrefix)
        {
            return ReflectionExtensions.GetLoadedAssemblies(projectNamespacePrefix);
        }

        private static IEnumerable<Assembly> GetLoadedAssemblies() => ((IEnumerable<Assembly>)AppDomain.CurrentDomain.GetAssemblies()).Where<Assembly>((Func<Assembly, bool>)(p => !p.IsDynamic));

        public static IEnumerable<Assembly> GetLoadedAssemblies(string prefix) => ReflectionExtensions.GetLoadedAssemblies().Where<Assembly>((Func<Assembly, bool>)(a => a.FullName.StartsWith(prefix)));

        public static List<AssemblyName> GetLoadedAndReferencedAssemblies(string prefix) => ReflectionExtensions.GetLoadedAssemblies().SelectMany<Assembly, AssemblyName>((Func<Assembly, IEnumerable<AssemblyName>>)(i => (IEnumerable<AssemblyName>)i.GetReferencedAssemblies())).Where<AssemblyName>((Func<AssemblyName, bool>)(i => i.FullName.Contains(prefix))).Distinct<AssemblyName>().ToList<AssemblyName>();

        public static Assembly GetAssembly(string name) => ReflectionExtensions.GetLoadedAssemblies().FirstOrDefault<Assembly>((Func<Assembly, bool>)(i => i.FullName.StartsWith(name)));

        public static Type GetDynamicType(string type)
        {
            Type type1 = Type.GetType(type);
            if (type1 == (Type)null)
                type1 = Type.GetType(type, (Func<AssemblyName, Assembly>)(name => ((IEnumerable<Assembly>)AppDomain.CurrentDomain.GetAssemblies()).FirstOrDefault<Assembly>((Func<Assembly, bool>)(z => z.FullName == name.FullName))), (Func<Assembly, string, bool, Type>)null, true);
            return type1;
        }

        public static IEnumerable<Type> GetAllImplementTypesOfGeneric(
            this Type type,
            string projectNamespacePrefix)
        {
            return ReflectionExtensions.GetProjectLoadedAssemblies(projectNamespacePrefix).SelectMany<Assembly, Type>((Func<Assembly, IEnumerable<Type>>)(s => (IEnumerable<Type>)s.GetTypes())).Where<Type>((Func<Type, bool>)(t => ((IEnumerable<Type>)t.GetInterfaces()).Any<Type>((Func<Type, bool>)(i => i.IsGenericType && i.GetGenericTypeDefinition() == type))));
        }

        public static IEnumerable<Type> GetAllImplementTypes(
            this Type interfaceType,
            string projectNamespacePrefix,
            bool includeAbstract = false)
        {
            try
            {
                return ReflectionExtensions.GetProjectLoadedAssemblies(projectNamespacePrefix).SelectMany<Assembly, Type>((Func<Assembly, IEnumerable<Type>>)(s => (IEnumerable<Type>)s.GetTypes())).Where<Type>((Func<Type, bool>)(x =>
                {
                    if (!interfaceType.IsAssignableFrom(x) || x.IsInterface)
                        return false;
                    return includeAbstract || !x.IsAbstract;
                }));
            }
            catch (ReflectionTypeLoadException ex)
            {
            }
            return (IEnumerable<Type>)new List<Type>();
        }

        public static List<PropertyInfo> GetPublicProperties(this Type type) => ((IEnumerable<PropertyInfo>)type.GetProperties(BindingFlags.Instance | BindingFlags.Public)).Where<PropertyInfo>((Func<PropertyInfo, bool>)(p => p.CanRead && p.CanWrite)).ToList<PropertyInfo>();

        public static List<PropertyInfo> GetPropertiesHaveAttribute(
            this Type type,
            Type attributeType)
        {
            return ((IEnumerable<PropertyInfo>)type.GetProperties()).Where<PropertyInfo>((Func<PropertyInfo, bool>)(p => p.GetCustomAttribute(attributeType) != null)).ToList<PropertyInfo>();
        }

        public static T GetAttribute<T>(this MemberInfo memberInfo) where T : class => ((IEnumerable<object>)memberInfo.GetCustomAttributes(typeof(T), false)).FirstOrDefault<object>((Func<object, bool>)(a => a is T)) as T;

        public static object GetPropertyValue<T>(this T entity, string propertyName)
        {
            PropertyInfo property = entity.GetType().GetProperty(propertyName);
            return property != (PropertyInfo)null ? property.GetValue((object)entity) : (object)null;
        }

        public static Decimal AdjustAndGetSumOfValuesBaseOnConfig<TModel, TConfig>(
            TModel model,
            TConfig config)
        {
            return ((IEnumerable<PropertyInfo>)config.GetType().GetProperties()).Sum<PropertyInfo>((Func<PropertyInfo, Decimal>)(configProperty => ReflectionExtensions.GetValueFromConfigProperty<TModel, TConfig>(model, config, configProperty)));
        }

        private static Decimal GetValueFromConfigProperty<TModel, TConfig>(
            TModel model,
            TConfig config,
            PropertyInfo configProperty)
        {
            Decimal num = 0M;
            PropertyInfo property = typeof(TModel).GetProperty(configProperty.Name);
            if (property == (PropertyInfo)null)
                return num;
            if ((bool)configProperty.GetValue((object)config))
                return (Decimal)property.GetValue((object)model);
            property.SetValue((object)model, (object)num);
            return num;
        }

        public static string GetPropertyName(PropertyInfo property)
        {
            object[] customAttributes1 = property.GetCustomAttributes(typeof(JsonPropertyAttribute), true);
            if (customAttributes1.Length != 0)
                return Enumerable.Cast<JsonPropertyAttribute>(customAttributes1).Single<JsonPropertyAttribute>().PropertyName;
            object[] customAttributes2 = property.GetCustomAttributes(typeof(DisplayNameAttribute), true);
            return customAttributes2.Length != 0 ? Enumerable.Cast<DisplayNameAttribute>(customAttributes2).Single<DisplayNameAttribute>().DisplayName : property.Name;
        }

        public static IEnumerable<string> GetAllClassName<T>(this IEnumerable<T> services) where T : class
        {
            foreach (T service in services)
                yield return service.ClassName<T>();
        }

        public static bool IsEnumerable(this Type type) => type is IEnumerable;

        public static string ClassName<T>(this T services) where T : class => typeof(T).IsEnumerable() ? string.Empty : services.GetType().Name;
    }
}