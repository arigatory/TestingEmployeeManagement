using EmployeeManagement.DataAccess.Entities;

namespace EmployeeManagement.Test;

public class EmployeeTests
{
    [Fact]
    public void EmployeeFullNamePropertyGetter_InputFirstNameAndLastName_FullNameIsConcatenation()
    {
        // Arrange
        var employee = new InternalEmployee("Ivan", "Panchenko", 0, 2500, false, 1);

        // Act
        employee.FirstName = "Anna";
        employee.LastName = "KARENINA";

        //Assert
        Assert.Equal("Anna Karenina", employee.FullName, ignoreCase: true);
    }

    [Fact]
    public void EmployeeFullNamePropertyGetter_InputFirstNameAndLastName_FullNameStartsWithFirstName()
    {
        // Arrange
        var employee = new InternalEmployee("Ivan", "Panchenko", 0, 2500, false, 1);

        // Act
        employee.FirstName = "Anna";
        employee.LastName = "Karenina";

        //Assert
        Assert.StartsWith(employee.FirstName, employee.FullName);
    }

    [Fact]
    public void EmployeeFullNamePropertyGetter_InputFirstNameAndLastName_FullNameEndsWithLastName()
    {
        // Arrange
        var employee = new InternalEmployee("Ivan", "Panchenko", 0, 2500, false, 1);

        // Act
        employee.FirstName = "Anna";
        employee.LastName = "Karenina";

        //Assert
        Assert.EndsWith(employee.LastName, employee.FullName);
    }

    [Fact]
    public void EmployeeFullNamePropertyGetter_InputFirstNameAndLastName_FullNameContainsPartOfConcatenation()
    {
        // Arrange
        var employee = new InternalEmployee("Ivan", "Panchenko", 0, 2500, false, 1);

        // Act
        employee.FirstName = "Anna";
        employee.LastName = "Karenina";

        //Assert
        Assert.Contains("na Ka", employee.FullName);
    }

    [Fact]
    public void EmployeeFullNamePropertyGetter_InputFirstNameAndLastName_FullNameSoundsLikeConcatenation()
    {
        // Arrange
        var employee = new InternalEmployee("Ivan", "Panchenko", 0, 2500, false, 1);

        // Act
        employee.FirstName = "Anna";
        employee.LastName = "Karenina";

        //Assert
        Assert.Matches("Anna K(o|a)renina", employee.FullName);
    }
}
