﻿using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Pocket.Common
{
    public static class Emit
    {
        public static Func<T> Ctor<T>() where T : new()
        {
            var type = typeof(T);
            var ctor = type.GetTypeInfo().GetConstructor(Array.Empty<Type>());
            var method = new DynamicMethod(type.Namespace + "_Ctor",
                type,
                Array.Empty<Type>());

            var il = method.GetILGenerator();

            il.DeclareLocal(type);

            il.Emit(OpCodes.Newobj, ctor);
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);

            return (Func<T>) method.CreateDelegate(typeof(Func<T>));
        }

        public static Func<T, object> GetField<T>(FieldInfo field)
        {
            if (field.DeclaringType == null)
                throw new ArgumentException($"Specified field `{field.Name}` must have declaring type.");
            
            var method = new DynamicMethod(field.DeclaringType.Namespace + "_" + field.Name + "_GetField",
                typeof(object),
                new[] { typeof(T) });

            var il = method.GetILGenerator();

            il.DeclareLocal(typeof(object));
            
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, field);
            
            if (field.FieldType.GetTypeInfo().IsValueType)
                il.Emit(OpCodes.Box, field.FieldType);
            
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);

            return (Func<T, object>) method.CreateDelegate(typeof(Func<T, object>));
        }

        public static Action<T, object> SetField<T>(FieldInfo field)
        {
            if (field.DeclaringType == null)
                throw new ArgumentException($"Specified field `{field.Name}` must have declaring type.");
            
            var method = new DynamicMethod(field.DeclaringType.Namespace + "_" + field.Name + "_SetField",
                typeof(void),
                new[] { typeof(T), typeof(object) });

            var il = method.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            
            il.Emit(OpCodes.Unbox_Any, field.FieldType);
            il.Emit(OpCodes.Stfld, field);
            il.Emit(OpCodes.Ret);

            return (Action<T, object>) method.CreateDelegate(typeof(Action<T, object>));
        }
    }
}