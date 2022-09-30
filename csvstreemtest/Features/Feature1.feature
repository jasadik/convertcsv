Feature: Converting csv file from url to XML and JSON File

A short summary of the feature

#@JSONScenario1
#Scenario: Convertion of csv file from invalid url to JSON
#	Given I Send the params url ("Not Valid URL") and type ("JSON")
#	And Send the request
#	Then Check The Result return error 400 And Check The ResultContent is :The CSV is badly formatted
#
#@XMLScenario1
#Scenario: Convertion of csv file from invalid url to XML
#	Given I Send the params url ("Not Valid URL") and type ("XML")
#	And Send the request
#	Then Check The Result return error 400 And Check The ResultContent is :The CSV is badly formatted

@JSONScenario2
Scenario: Convertion of csv file from valid url to JSON
	Given I Send the params url ("https://ohzard.com/files/exemplecsv.txt") and type ("JSON")
	And Send the request
	Then Check The Result return 200 And Check The ResultContent is ValidJSON

@XMLScenario2
Scenario: Convertion of csv file from valid url to XML
	Given I Send the params url ("https://ohzard.com/files/exemplecsv.txt") and type ("XML")
	And Send the request
	Then Check The Result return 200 And Check The ResultContent is ValidXML


