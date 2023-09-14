using BrewUp.Modules.Purchases.Domain;
using NetArchTest.Rules;

namespace BrewUp.Modules.Purchases.NetArchTests;

public class VerifyArchitectureTest
{
	[Fact]
	public void Should_Architecture_BeCompliant()
	{
		var types = Types.InAssembly(typeof(Program).Assembly);

		var assembliesList = new List<string>
		{
			"BrewUp.Modules.Purchases.Domain",
			"BrewUp.Modules.Purchases.Messages",
			"BrewUp.Modules.Purchases.ReadModel",
			"BrewUp.Modules.Purchases.SharedKernel"
		};

		var result = types
			.ShouldNot()
			.HaveDependencyOnAny(assembliesList.ToArray())
			.GetResult()
			.IsSuccessful;

		Assert.True(result);
	}

	[Fact]
	public void Should_Presentation_HasDependency_Only_With_Facade()
	{
		var types = Types.InAssembly(typeof(Program).Assembly)
			.That()
			.ArePublic()
			.And()
			.AreClasses()
			.And()
			.AreNotAbstract()
			.And()
			.ResideInNamespaceStartingWith("BrewUp.Modules");

		var dependenciesAssemblies = new List<string>
		{
			"BrewUp.Modules.Purchases",
			"BrewUp.Modules.Warehouses",
			"BrewUp.Modules.Sagas"
		}.ToArray();

		var result = types
			.Should()
			.NotHaveDependencyOnAny(dependenciesAssemblies)
			.GetResult()
			.FailingTypeNames;

		Assert.True(result.Count.Equals(dependenciesAssemblies.ToArray().Length));
	}

	[Fact]
	// Classes in the Domain should not directly reference ReadModel
	public void Domain_ShouldNot_HavingReferenceTo_Facade_And_ReadModel()
	{
		var types = Types.InAssembly(typeof(PurchasesDomainHelper).Assembly)
			.That()
			.ResideInNamespace("BrewUp.Modules.Purchases.Domain");

		var result = types
			.ShouldNot()
			.HaveDependencyOn("BrewUp.Modules.Purchases")
			.And()
			.HaveDependencyOn("BrewUp.Modules.Purchases.ReadModel")
			.GetResult()
			.IsSuccessful;

		Assert.True(result);
	}
}