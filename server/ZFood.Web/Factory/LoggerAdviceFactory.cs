using ZFood.Web.Logger;

namespace ZFood.Web.Factory
{
    public static class LoggerAdviceFactory<T>
    {
        public static T CreateLoggerAdvice(T objectToBeDecorated)
        {
            return new LoggerAdvice<T>().Create(objectToBeDecorated);
        }
    }
}
