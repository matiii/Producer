namespace Producer
{
    using System;
    using System.Linq;
    using System.Reflection.Emit;

    public static class Producer
    {

        /// <summary>
        /// Produce delagate which allow to create new instance.
        /// </summary>
        /// <typeparam name="T">Class to create.</typeparam>
        /// <param name="parameters">Parameters in T constructor.</param>
        /// <returns>Delegate to create instance of T.</returns>
        public static Func<object[], T> Produce<T>(params Type[] parameters) where T : class
        {
            Type type = typeof(T);
            var dynamicMethod = new DynamicMethod("Ctor_" + type.FullName, type, new [] { typeof(object[]) }, true);
            var ilGenerator = dynamicMethod.GetILGenerator();

            var ctorInfo = type.GetConstructor(parameters);

            if (ctorInfo != null)
            {
                LocalBuilder[] locals = parameters.Select(x => ilGenerator.DeclareLocal(x)).ToArray();

                for (int i = 0; i < parameters.Length; i++)
                {
                    ilGenerator.Emit(OpCodes.Ldarg_0);
                    ilGenerator.Emit(OpCodes.Ldc_I4, i);
                    ilGenerator.Emit(OpCodes.Ldelem_Ref);
                    ilGenerator.Emit(OpCodes.Unbox_Any, parameters[i]);
                    ilGenerator.Emit(OpCodes.Stloc, locals[i]);
                }

                for (int i = 0; i < parameters.Length; i++)
                    ilGenerator.Emit(OpCodes.Ldloc, i);

                ilGenerator.Emit(OpCodes.Newobj, ctorInfo);
                ilGenerator.Emit(OpCodes.Ret);

                object del = dynamicMethod.CreateDelegate(typeof(Func<object[], T>));
                return (Func<object[], T>) del;
            }

            return default(Func<object[], T>);
        }

        /// <summary>
        /// Produce delagate which allow to create new instance with empty constructor.
        /// </summary>
        /// <typeparam name="T">Class to create.</typeparam>
        /// <returns>Delegate to create instance of T.</returns>
        public static Func<object[], T> Produce<T>() where T : class => Produce<T>(Type.EmptyTypes);
    }
}
