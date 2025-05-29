Feature: Create User

This feature is about creating a new user.

There are 3 validations:
* User email must be unique
* Email must be valid
* Password must be at least 8 characters long with numbers, letters and special characters

Rule: The email for the user must be valid and the password must be at least 8 characters long with numbers, letters and special characters.

@success @user @create
Scenario: User is created successfully
	Given the user started the registration
	And add the values to the user
		| email             | password | firstName | lastName | id |
		| email@correto.com | P4$$w0rd | FirstName | LastName | 0 |
	When the client sends the request
	Then the user is created and the id is returned

@error @user @create @email
Scenario: Email is not valid
	Given the user started the registration
	And add the values to the user
		| email | password | firstName | lastName | id |
		| email | P4$$w0rd | FirstName | LastName | 0 |
	When the client sends the request
	Then the user is not created with the message "Invalid email format"

@error @user @create @password
Scenario: Password is not valid
	Given the user started the registration
	And add the values to the user
		| email             | password | firstName | lastName | id |
		| email@correto.com | password | FirstName | LastName | 0 |
	When the client sends the request
	Then the user is not created with the message "Invalid password format"

@error @user @create @username
Scenario: Email already exists
	Given the user started the registration
	And add the values to the user
		| email             | password | firstName | lastName | id |
		| email@correto.com | P4$$w0rd | FirstName | LastName | 0 |
	And the user already exists in the system
	When the client sends the request
	Then the user is not created with the message "User with this email already exists"
