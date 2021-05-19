Feature: CountryC
![Calculator](https://specflow.org/wp-content/uploads/2020/09/calculator.png)
Simple calculator for adding **two** numbers

Link to a feature: [Calculator](MongoPoc.Specs/Features/Calculator.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@mytag
Scenario: Add new countryC1
	When I start the application
	When I add new country with name 'Israel' and id 1
	Then the response should be 1

@mytag
Scenario: Add new countryC2
	When I start the application
	When I add new country with name 'Israel' and id 1
	Then the response should be 1

	@mytag
Scenario: Add new countryC3
	When I start the application
	When I add new country with name 'Israel' and id 1
	Then the response should be 1

	@mytag
Scenario: Add new countryC4
	When I start the application
	When I add new country with name 'Israel' and id 1
	Then the response should be 1

	@mytag
Scenario: Add new countryC5
	When I start the application
	When I add new country with name 'Israel' and id 1
	Then the response should be 1
