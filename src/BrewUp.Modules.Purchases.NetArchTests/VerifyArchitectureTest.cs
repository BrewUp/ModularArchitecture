using BrewUp.Modules.Purchases.Domain;
using NetArchTest.Rules;

namespace BrewUp.Modules.Purchases.NetArchTests;

public class VerifyArchitectureTest
{
	[Fact]
	public void Should_Architecture_BeCompliant()
	{
		var types = Types.InAssembly(typeof(Program).Assembly);

		var forbiddenAssemblies = new List<string>
		{
			"BrewUp.Modules.Purchases.Domain",
			"BrewUp.Modules.Purchases.Messages",
			"BrewUp.Modules.Purchases.ReadModel",
			"BrewUp.Modules.Purchases.SharedKernel",
			"BrewUp.Modules.Warehouses.Domain",
			"BrewUp.Modules.Warehouses.ReadModel"
		};

		var result = types
			.ShouldNot()
			.HaveDependencyOnAny(forbiddenAssemblies.ToArray())
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

		var authorizedAssemblies = new List<string>
		{
			"BrewUp.Modules.Purchases",
			"BrewUp.Modules.Purchases.Provider",
			"BrewUp.Modules.Warehouses",
			"BrewUp.Modules.Warehouses.Provider",
			"BrewUp.Modules.Sagas"
		}.ToArray();

		var result = types
			.Should()
			.HaveDependencyOnAny(authorizedAssemblies)
			.GetResult()
			.IsSuccessful;

		Assert.True(result);
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
	
	[Fact]
	// Classes in the module Purchases should not directly reference Sagas
	public void Purchases_ShouldNot_HavingReferenceTo_Sagas()
	{
		var types = Types.InAssembly(typeof(PurchasesDomainHelper).Assembly)
			.That()
			.ResideInNamespaceEndingWith("BrewUp.Modules.Purchases");

		var result = types
			.ShouldNot()
			.HaveDependencyOn("BrewUp.Modules.Sagas")
			.GetResult()
			.IsSuccessful;

		Assert.True(result);
	}
	
	[Fact]
	// Classes in the module Purchases should not directly reference Warehouses
	public void Purchases_ShouldNot_HavingReferenceTo_Warehouses()
	{
		var types = Types.InAssembly(typeof(PurchasesDomainHelper).Assembly)
			.That()
			.ResideInNamespaceEndingWith("BrewUp.Modules.Purchases");

		var result = types
			.ShouldNot()
			.HaveDependencyOn("BrewUp.Modules.Warehouses")
			.And()
			.HaveDependencyOn("BrewUp.Modules.Warehouses.Domain")
			.And()
			.HaveDependencyOn("BrewUp.Modules.Warehouses.Provider")
			.And()
			.HaveDependencyOn("BrewUp.Modules.Warehouses.ReadModel")
			.GetResult()
			.IsSuccessful;

		Assert.True(result);
	}
}