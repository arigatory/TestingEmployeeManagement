using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;

namespace EmployeeManagement.Test;

public class EmployeeFactoryTests : IDisposable
{
    private EmployeeFactory _employeeFactory;

    public EmployeeFactoryTests()
    {
        _employeeFactory = new EmployeeFactory();
    }

    public void Dispose()
    {
        // clean up the setup code, if required
    }


    [Fact]
    public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500()
    {
        var employee = (InternalEmployee) _employeeFactory.CreateEmployee("Ivan", "Panchenko");

        Assert.Equal(2500, employee.Salary);
    }

    [Fact]
    public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500()
    {

        var employee = (InternalEmployee)_employeeFactory.CreateEmployee("Ivan", "Panchenko");

        Assert.True(employee.Salary >= 2500 && employee.Salary <= 3500,
            "Salary not in acceptable range.");
    }


    [Fact]
    public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500AlternativeWay()
    {

        var employee = (InternalEmployee)_employeeFactory.CreateEmployee("Ivan", "Panchenko");

        Assert.True(employee.Salary >= 2500);
        Assert.True(employee.Salary <= 3500);
    }

    [Fact]
    public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500WithInRange()
    {

        var employee = (InternalEmployee)_employeeFactory.CreateEmployee("Ivan", "Panchenko");

        Assert.InRange(employee.Salary, 2500, 3000);
    }

    [Fact]
    public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500_PrecisionExample()
    {

        var employee = (InternalEmployee)_employeeFactory.CreateEmployee("Ivan", "Panchenko");
        employee.Salary = 2500.123m;

        Assert.Equal(2500,employee.Salary, 0);
    }

    [Fact]
    public void CreateEmployee_IsExternalIsTrue_ReturnTypeMustBeExternalEmployee()
    {
        // Arrange

        // Act
        var employee = _employeeFactory.CreateEmployee("Ivan", "Panchenko", "MSU", true);

        // Assert
        Assert.IsType<ExternalEmployee>(employee);
        //Assert.IsAssignableFrom<Employee>(employee);
    }

}