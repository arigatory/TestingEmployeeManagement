using EmployeeManagement.Business;

namespace EmployeeManagement.Test.Fixtures
{
    public class EmployeeServiceFixture : IDisposable
    {
        public EmployeeManagementTestDataRepository EmployeeManagementTestDataRepository { get; }
        public EmployeeService EmployeeService { get; }

        public EmployeeServiceFixture()
        {
            EmployeeManagementTestDataRepository = 
                new EmployeeManagementTestDataRepository();
            EmployeeService = new EmployeeService(
                EmployeeManagementTestDataRepository, new EmployeeFactory());
        }

        public void Dispose()
        {
            // clean if need
        }
    }
}
