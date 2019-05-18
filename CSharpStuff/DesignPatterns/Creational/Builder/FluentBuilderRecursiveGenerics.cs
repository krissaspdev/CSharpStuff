using System;

namespace CSharpStuff.DesignPatterns.Creational.Builder
{
    public class Employee
    {
        public string Name;
        public int Age;

        public string Position;

        public int Salary;

        public class Builder : EmployeeSalaryBuilder<Builder>
        {

        }

        public static Builder New => new Builder();

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}, {nameof(Position)}: {Position}";
        }
    }

    public abstract class EmployeeBuilder
    {
        protected Employee employee = new Employee();

        public Employee Build()
        {
            return employee;
        }
    }

    public class EmployeeInfoBuilder<SELF> : EmployeeBuilder
    where SELF : EmployeeInfoBuilder<SELF>
    {
        public SELF Called(string name)
        {
            employee.Name = name;
            return (SELF)this;
        }

        public SELF WithAge(int age)
        {
            employee.Age = age;
            return (SELF)this;
        }

    }

    public class EmployeeJobBuilder<SELF> : EmployeeInfoBuilder<EmployeeJobBuilder<SELF>>
        where SELF : EmployeeJobBuilder<SELF>
    {
        public SELF WorksAs(string position)
        {
            employee.Position = position;
            return (SELF)this;
        }
    }

    public class EmployeeSalaryBuilder<SELF> : EmployeeJobBuilder<EmployeeSalaryBuilder<SELF>>
        where SELF : EmployeeSalaryBuilder<SELF>
    {
        public SELF WithSalary(int salary)
        {
            employee.Salary = salary;
            return (SELF)this;
        }
    }


    public static class FluentBuilderRunner
    {
        public static void Run()
        {
            var employee = Employee.New
                .Called("Kris")
                .WorksAs("Manager").WithSalary(100)
                .WithAge(25)
                .Build();

            Console.WriteLine(employee);
        }
    }

}