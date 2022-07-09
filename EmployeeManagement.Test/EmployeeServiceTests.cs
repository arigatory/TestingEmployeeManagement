﻿using EmployeeManagement.Business;
using EmployeeManagement.Business.EventArguments;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.DataAccess.Entities;

namespace EmployeeManagement.Test;

public class EmployeeServiceTests
{
    [Fact]
    public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveFirstObligatoryCourse_WithObject()
    {
        // Arrange
        var employeeManagementTestDataRepository = new EmployeeManagementTestDataRepository();
        var employeeService = new EmployeeService(employeeManagementTestDataRepository, new EmployeeFactory());
        var obligatoryCourse = employeeManagementTestDataRepository.GetCourse(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));

        // Act
        var internalEmployee = employeeService.CreateInternalEmployee("Tomodo", "Yamato");

        //Assert
        Assert.Contains(obligatoryCourse, internalEmployee.AttendedCourses);
    }

    [Fact]
    public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveFirstObligatoryCourse_WithPredicate()
    {
        // Arrange
        var employeeManagementTestDataRepository = new EmployeeManagementTestDataRepository();
        var employeeService = new EmployeeService(employeeManagementTestDataRepository, new EmployeeFactory());

        // Act
        var internalEmployee = employeeService.CreateInternalEmployee("Tomodo", "Yamato");

        //Assert
        Assert.Contains(internalEmployee.AttendedCourses,
            course => course.Id == Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));
    }

    [Fact]
    public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveSecondObligatoryCourse_WithPredicate()
    {
        // Arrange
        var employeeManagementTestDataRepository = new EmployeeManagementTestDataRepository();
        var employeeService = new EmployeeService(employeeManagementTestDataRepository, new EmployeeFactory());

        // Act
        var internalEmployee = employeeService.CreateInternalEmployee("Tomodo", "Yamato");

        //Assert
        Assert.Contains(internalEmployee.AttendedCourses,
            course => course.Id == Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));
    }

    [Fact]
    public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustMatchObligatoryCourses()
    {
        // Arrange
        var employeeManagementTestDataRepository = new EmployeeManagementTestDataRepository();
        var employeeService = new EmployeeService(employeeManagementTestDataRepository, new EmployeeFactory());
        var obligatoryCourses = employeeManagementTestDataRepository.GetCourses(
            Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
            Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

        // Act
        var internalEmployee = employeeService.CreateInternalEmployee("Tomodo", "Yamato");

        //Assert
        Assert.Equal(obligatoryCourses, internalEmployee.AttendedCourses);
    }

    [Fact]
    public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustNotBeNew()
    {
        // Arrange
        var employeeManagementTestDataRepository = new EmployeeManagementTestDataRepository();
        var employeeService = new EmployeeService(employeeManagementTestDataRepository, new EmployeeFactory());
        var obligatoryCourses = employeeManagementTestDataRepository.GetCourses(
            Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
            Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

        // Act
        var internalEmployee = employeeService.CreateInternalEmployee("Tomodo", "Yamato");

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
        var employeeManagementTestDataRepository = new EmployeeManagementTestDataRepository();
        var employeeService = new EmployeeService(employeeManagementTestDataRepository, new EmployeeFactory());
        var obligatoryCourses = await employeeManagementTestDataRepository.GetCoursesAsync(
            Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
            Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

        // Act
        var internalEmployee = await employeeService.CreateInternalEmployeeAsync("Tomodo", "Yamato");

        //Assert
        Assert.Equal(obligatoryCourses, internalEmployee.AttendedCourses);
    }

    [Fact]
    public async Task GiveRaise_RaiseBelowMinimumGiven_EmployeeInvalidRaiseExceptionMustBeThrown()
    {
        // Arrange
        var employeeService = new EmployeeService(
            new EmployeeManagementTestDataRepository(),
            new EmployeeFactory());
        var internalEmployee = new InternalEmployee(
            "Ivan", "Panchenko", 5, 3000, false, 1);


        // Act & Assert
        await Assert.ThrowsAsync<EmployeeInvalidRaiseException>(
            async () =>
            await employeeService.GiveRaiseAsync(internalEmployee, 50));
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