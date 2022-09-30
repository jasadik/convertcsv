Feature: Converting csv file from url to XML and JSON File

A short summary of the feature

@JSONScenario1
Scenario: Convertion of csv file from invalid url to JSON
	Given I Send the params url ("Not Valid URL") and type ("JSON")
	And Send the request
	Then Check The Result return error 400 And Check The ResultContent is ("It's a bad request")

