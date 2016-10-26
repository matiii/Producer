namespace Producer
{
    using System;
    using System.Linq;
    using System.Reflection.Emit;

    public static class Producer
    {
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
    }
}
