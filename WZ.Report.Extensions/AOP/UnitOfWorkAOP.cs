using Castle.DynamicProxy;
using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WZ.Report.Common.Attribute;
using WZ.Report.Model;

namespace WZ.Report.Extensions.AOP
{
    public class UnitOfWorkAOP : IInterceptor
    {
        public UnitOfWorkAOP()
        {

        }
        public void Intercept(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;          
            if (method.GetCustomAttributes(true).FirstOrDefault(x => x.GetType() == typeof(UseTranAttribute)) is UseTranAttribute)
            {
                Console.WriteLine($"Begin Transaction");
                var uowManager = new UnitOfWorkManager(Reportcontext.freeSql);
                uowManager.Begin();
                try
                {          
                    invocation.Proceed();
                    // 异步获取异常，先执行
                    if (IsAsyncMethod(invocation.Method))
                    {
                        var result = invocation.ReturnValue;
                        if (result is Task)
                        {
                            Task.WaitAll(result as Task);
                        }
                    }
                    uowManager.Current.Commit();
                }
                catch (Exception)
                {
                    Console.WriteLine($"Rollback Transaction");
                    uowManager.Current.Rollback();
                }
            }
            else
            {
                invocation.Proceed();//直接执行被拦截方法
            }         
        }


        private async Task SuccessAction(IInvocation invocation)
        {
            await Task.Run(() =>
            {
                //...
            });
        }

        public static bool IsAsyncMethod(MethodInfo method)
        {
            return (
                method.ReturnType == typeof(Task) ||
                (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
                );
        }
        private async Task TestActionAsync(IInvocation invocation)
        {
            await Task.Run(null);
        }
    }
}
