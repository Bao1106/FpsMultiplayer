using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Services.Utils;
using UnityEngine;

namespace Services.DependencyInjection
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class InjectAttribute : Attribute
    {
        public InjectAttribute()
        {
            
        }
    }
    
    [AttributeUsage(AttributeTargets.All)]
    public sealed class ProvideAttribute : Attribute
    {
        public ProvideAttribute()
        {
            
        }
    }
    
    public interface IDependencyProvider{}

    public class Injector : Singleton<Injector>
    {
        private const BindingFlags _bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        private readonly Dictionary<Type, object> registry = new();

        protected override void Awake()
        {
            base.Awake();
            
            InitializeInjector();
        }

        public void InitializeInjector()
        {
            //Find all modules implementing IDependencyProvider
            var providers = FindMonoBehaviours()
                .OfType<IDependencyProvider>();

            foreach (var provider in providers)
            {
                RegisterProvider(provider);
            }
            
            //Find all injectable objects and inject their dependencies
            var injectables = FindMonoBehaviours().Where(IsInjectable);
            foreach (var injectable in injectables)
            {
                Inject(injectable);
            }
        }
        
        private void Inject(object instance)
        {
            var type = instance.GetType();
            var injectableFields = type.GetFields(_bindingFlags)
                .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

            foreach (var injectableField in injectableFields)
            {
                var fieldType = injectableField.FieldType;
                var resolvedInstance = Resolve(fieldType);
                if (resolvedInstance == null)
                    throw new Exception($"Failed to inject {fieldType.Name} into {type.Name}");
                
                injectableField.SetValue(instance, resolvedInstance);
                Debug.Log($"Injected {fieldType.Name} into {type.Name}");
            }
        }

        private object Resolve(Type type)
        {
            registry.TryGetValue(type, out var resolvedInstance);
            return resolvedInstance;
        }
        
        private static bool IsInjectable(MonoBehaviour obj)
        {
            var members = obj.GetType().GetMembers(_bindingFlags);
            return members.Any(member => Attribute.IsDefined(member, typeof(InjectAttribute)));
        }
        
        private void RegisterProvider(IDependencyProvider provider)
        {
            var methods = provider.GetType().GetMethods(_bindingFlags);

            foreach (var method in methods)
            {
                if (!Attribute.IsDefined(method, typeof(ProvideAttribute))) continue;

                var returnType = method.ReturnType;
                var providerInstance = method.Invoke(provider, null);
                if (providerInstance != null)
                {
                    if (registry.TryAdd(returnType, providerInstance))
                    {
                        Debug.Log($"Registered {returnType.Name} from {provider.GetType().Name}");
                    }
                }
                else
                {
                    throw new Exception($"Provider {provider.GetType().Name} returned null for {returnType.Name}");
                }
            }
        }

        private static MonoBehaviour[] FindMonoBehaviours()
        {
            return FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID);
        }
    }
}
