using System.Linq;
using System.Reflection;
using NetArchTest.Rules;
using NUnit.Framework;
using Shopping.Application;
using Shopping.Domain;

namespace Shopping.ArchitectureTests;

public class ApplicationTests : TestBase
{
    private static Assembly ApplicationAssembly => typeof(DependencyInstallers).Assembly;
    
    [Test]
    public void ShouldNotHaveDependencyOnInfrastructureLayer()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .ResideInNamespace("Shopping.Application")
            .ShouldNot()
            .HaveDependencyOn("Shopping.Infrastructure")
            .GetResult();
        
        Assert.That(result.IsSuccessful, Is.True, "Application layer should not have a dependency on the Infrastructure layer");
    }

    [Test]
    public void ShouldFollowRepositoryNamingConvention()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That().ResideInNamespaceStartingWith("Shopping.Application")
            .And().ImplementInterface(typeof(IRepository<>))
            .And().AreNotGeneric()
            .And().AreInterfaces()
            .Should().HaveNameEndingWith("Repository")
            .GetResult();
        
        Assert.That(result.IsSuccessful, Is.True, "Application layer interfaces should end with Repository naming convention");
    }
    
    [Test]
    public void ShouldNotImplementRepository()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That().ResideInNamespaceStartingWith("Shopping.Application")
            .And().AreNotGeneric()
            .And().AreInterfaces()
            .And().HaveName("IEventPublisher")
            .Should().NotImplementInterface(typeof(IRepository<>))
            .GetResult();
        
        Assert.That(result.IsSuccessful, Is.True, "Application layer event publisher should not implement repository interface");
    }
}