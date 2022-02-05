using DataInterface;
using LogicInterface;
using MyLogic;
using TestMemoryData;
using TestSQlite;

namespace Dependencies
{
    public class MyDependencyResolver
    {
        private static IData _iData;
        public static IData IData => _iData ?? (_iData = new MemoryData());

        private static ILogic _iLogic;
        public static ILogic ILogic => _iLogic ?? (_iLogic = new Logic(IData));

    }
}
