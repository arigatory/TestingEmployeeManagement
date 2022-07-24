using EmployeeManagement.Business;
using EmployeeManagement.Business.EventArguments;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Test.Fixtures;

namespace EmployeeManagement.Test;

[Collection("EmployeeServiceCollection")]
public class EmployeeServiceTests 
{
    private readonly EmployeeServiceFixture _employeeServiceFixture;

    public EmployeeServiceTests(EmployeeServiceFixture employeeServiceFixture)
    {
        _employeeServiceFixture = employeeServiceFixture;
    }

    [Fact]
    public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveFirstObligatoryCourse_WithObject()
    {
        // Arrange
        var obligatoryCourse = _employeeServiceFixture.EmployeeManagementTestDataRepository
            .GetCourse(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));

        // Act
        var internalEmployee = _employeeServiceFixture.EmployeeService.CreateInternalEmployee("Tomodo", "Yamato");

        //Assert
        Assert.Contains(obligatoryCourse, internalEmployee.AttendedCourses);
    }

    [Fact]
    public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveFirstObligatoryCourse_WithPredicate()
    {
        // Arrange


        // Act
        var internalEmployee = _employeeServiceFixture.EmployeeService.CreateInternalEmployee("Tomodo", "Yamato");

        //Assert
        Assert.Contains(internalEmployee.AttendedCourses,
            course => course.Id == Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));
    }

    [Fact]
    public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveSecondObligatoryCourse_WithPredicate()
    {
        // Arrange

        // Act
        var internalEmployee = _employeeServiceFixture.EmployeeService.CreateInternalEmployee("Tomodo", "Yamato");

        //Assert
        Assert.Contains(internalEmployee.AttendedCourses,
            course => course.Id == Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));
    }

    [Fact]
    public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustMatchObligatoryCourses()
    {
        // Arrange
        var obligatoryCourses = _employeeServiceFixture.EmployeeManagementTestDataRepository.GetCourses(
            Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
            Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

        // Act
        var internalEmployee = _employeeServiceFixture.EmployeeService.CreateInternalEmployee("Tomodo", "Yamato");

        //Assert
        Assert.Equal(obligatoryCourses, internalEmployee.AttendedCourses);
    }

    [Fact]
    public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustNotBeNew()
    {
        // Arrange
        var obligatoryCourses = _employeeServiceFixture.EmployeeManagementTestDataRepository.GetCourses(
            Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
            Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

        // Act
        var internalEmployee = _employeeServiceFixture.EmployeeService.CreateInternalEmployee("Tomodo", "Yamato");

        //Assert
        foreach (var course in internalEmployee.AttendedCourses)
        {
            Assert.False(course.IsNew);
        }

        Assert.All(internalEmployee.AttendedCourses, course => Assert.False(course.IsNew));
    }

    [Fact]
    public async Task CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustNotBeNew_Async()
    {
        // Arrange
        var obligatoryCourses = await _employeeServiceFixture.EmployeeManagementTestDataRepository.GetCoursesAsync(
            Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
            Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

        // Act
        var internalEmployee = await _employeeServiceFixture.EmployeeService.CreateInternalEmployeeAsync("Tomodo", "Yamato");

        //Assert
        Assert.Equal(obligatoryCourses, internalEmployee.AttendedCourses);
    }

    [Fact]
    public async Task GiveRaise_RaiseBelowMinimumGiven_EmployeeInvalidRaiseExceptionMustBeThrown()
    {
        // Arrange
        var internalEmployee = new InternalEmployee(
            "Ivan", "Panchenko", 5, 3000, false, 1);


        // Act & Assert
        await Assert.ThrowsAsync<EmployeeInvalidRaiseException>(
            async () =>
            await _employeeServiceFixture.EmployeeService.GiveRaiseAsync(internalEmployee, 50));
    }

    [Fact]
    public void NotifyOfAbsence_EmployeeIsAbsent_OnEmmployeeIsAbsentMustBeTriggered()
    {
        // Arrange
        var employeeService = new EmployeeService(
            new EmployeeManagementTestDataRepository(),
            new EmployeeFactory());
        var internalEmployee = new InternalEmployee(
            "Ivan", "Panchenko", 5, 3000, false, 1);

        // Act & Assert
        Assert.Raises<EmployeeIsAbsentEventArgs>(
            handler => employeeService.EmployeeIsAbsent += handler,
            handler => employeeService.EmployeeIsAbsent -= handler,
            () => employeeService.NotifyOfAbsence(internalEmployee));
    }

    
}
