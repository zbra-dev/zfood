using log4net;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZFood.Web.Logger
{
    public class LoggerAdvice<T> : DispatchProxy
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LoggerAdvice<>));

        private T decoratedObject;

        public T Create(T decoratedObject)
        {
            object proxy = Create<T, LoggerAdvice<T>>();
            ((LoggerAdvice<T>)proxy).SetParameters(decoratedObject);

            return (T)proxy;
        }

        private void SetParameters(T decoratedObject)
        {
            if (decoratedObject == null)
            {
                throw new ArgumentNullException(nameof(decoratedObject));
            }

            this.decoratedObject = decoratedObject;
        }

        protected override object Invoke(MethodInfo targetMethod, object[] arguments)
        {
            if (targetMethod == null)
            {
                throw new ArgumentNullException(nameof(targetMethod));
            }

            var stopwatch = new Stopwatch();
            Exception exception = null;
            try
            {
                LogBeginInvoke(targetMethod, arguments);
                stopwatch.Start();
                var result = targetMethod.Invoke(decoratedObject, arguments);
                if (result is Task resultTask && resultTask.IsFaulted)
                {
                    exception = resultTask.Exception;
                }
                return result;
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                stopwatch.Stop();
                if (exception == null)
                {
                    LogEndInvoke(targetMethod, arguments, stopwatch);
                }
                else
                {
                    LogError(targetMethod, exception, stopwatch);
                }
            }
        }

        private void LogBeginInvoke(MethodInfo methodInfo, object[] arguments)
        {
            var argumentsList = BuildArgumentsList(arguments);
            log.Debug($"[Begin Invoke: Class '{decoratedObject.GetType().Name}', " +
                $"Method: [{methodInfo.Name}] with arguments [{argumentsList}]]");
        }

        private string BuildArgumentsList(object[] arguments)
        {
            var argumentsList = new StringBuilder();
            if (!arguments.Any())
            {
                argumentsList.Append("none");
            }
            else
            {
                foreach (var argument in arguments)
                {
                    argumentsList.Append($"{argument}");
                    argumentsList.Append(" ");
                }
            }

            return argumentsList.ToString();
        }

        private void LogEndInvoke(MethodInfo methodInfo, object[] arguments, Stopwatch stopwatch)
        {
            log.Debug($"[End Invoke: Class '{decoratedObject.GetType().Name}', " +
                $"Method [{methodInfo.Name}] " +
                $"in [{stopwatch.ElapsedMilliseconds}] ms]");
        }

        private void LogError(MethodInfo methodInfo, Exception ex, Stopwatch stopwatch)
        {
            log.Error($"[End Invoke: Class '{decoratedObject.GetType().Name}', " +
                $"Method: [{methodInfo.Name}] " +
                $"in [{stopwatch.ElapsedMilliseconds}] ms " +
                $"with errors [{ex.Message}]]");
        }
    }
}
