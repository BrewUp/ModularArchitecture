using NetArchTest.Rules;

namespace BrewUp.Modules.Purchases.NetArchTests;

public class VerifyArchitectureTest
{
	[Fact]
	public void Should_Architecture_BeCompliant()
	{
		var result = Types.InCurrentDomain()
			.That()
			.ResideInNamespace("BrewUp.Modules.Purchases")
			.ShouldNot()
			.HaveDependencyOn("BrewUp.Modules.Warehouses")
			.GetResult()
			.IsSuccessful;

		Assert.True(result);
	}

	[Fact]
	public void ShouldNot_Domain_HavingReferenceToReadModel()
	{
		var result = Types.InCurrentDomain()
			.That()
			.ResideInNamespace("BrewUp.Modules.Purchases.Domain")
			.ShouldNot()
			.HaveDependencyOn("BrewUp.Modules.Purchases.ReadModel")
			.GetResult()
			.IsSuccessful;

		Assert.True(result);
	}
}