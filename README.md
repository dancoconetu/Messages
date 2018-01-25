# Messages API 
Design and build a RESTful API to work as a basic message store.

It should include these features:
  - a client can create a message in the service
  - the same client can modify their message
  - the same client can delete their message
  - a client can view any message available

Use an in-memory solution for storing any data for this service. Consider the appropriate HTTP verbs, headers and responses to use.
## Installation guide
Clone this github repository and open in in Visual Studio 2017. Build and run the project in a browser (tested it with Chrome and Edge). 
If the project cannot be built, make sure that System.Runtime.Serialization is added as a refference and Swashbuckle.Core as a NuGet package. 
## Documentation
The documentation for this web api can be found at http://localhost:{port}/swagger/ui/index#/Messages. 
## Webservices
The available Rest Verbs are Get, Post, Put and Delete, found at http://localhost:{port}/api/messages. All the Post, Put and Delete methods need Basic authentication in order for a user to use them. Any random authentication is fine, once a user and password is written as basic authentication, another user cannot user the same for the username without the password. 

## More explanations
I have used authentication to make sure that only the same client can modify or delete their own message. I save the authenticated users inside a Dictionary inside a class based on singleton pattern. The same way I save the messages, inside a list inside a class based on singleton pattern. 

