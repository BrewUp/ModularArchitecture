using NetArchTest.Rules;
using Xunit;

namespace BrewUp.Modules.Warehouses.NetArchTests;

public class VerifyArchitectureTest
{
	[Fact]
	public void Should_Architecture_BeCompliant()
	{
		var result = Types.InCurrentDomain()
			.That()
			.ResideInNamespace("BrewUp.Modules.Warehouses")
			.ShouldNot()
			.HaveDependencyOn("BrewUp.Modules.Purchases")
			.GetResult()
			.IsSuccessful;

		Assert.True(result);
	}

	[Fact]
	public void ShouldNot_Domain_HavingReferenceToReadModel()
	{
		var result = Types.InCurrentDomain()
			.That()
			.ResideInNamespace("BrewUp.Modules.Warehouses.Domain")
			.ShouldNot()
			.HaveDependencyOn("BrewUp.Modules.Warehouses.ReadModel")
			.GetResult()
			.IsSuccessful;

		Assert.True(result);
	}
}