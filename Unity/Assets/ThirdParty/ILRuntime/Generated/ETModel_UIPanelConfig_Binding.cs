using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class ETModel_UIPanelConfig_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(ETModel.UIPanelConfig);

            field = type.GetField("WindowLayer", flag);
            app.RegisterCLRFieldGetter(field, get_WindowLayer_0);
            app.RegisterCLRFieldSetter(field, set_WindowLayer_0);


        }



        static object get_WindowLayer_0(ref object o)
        {
            return ((ETModel.UIPanelConfig)o).WindowLayer;
        }
        static void set_WindowLayer_0(ref object o, object v)
        {
            ((ETModel.UIPanelConfig)o).WindowLayer = (System.String)v;
        }


    }
}
