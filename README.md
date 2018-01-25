# Messages API

## Installation guide
Clone this github repository and open in in Visual Studio 2017. Build and run the project in a browser (tested it with Chrome and Edge). 
If the project cannot be built, make sure that System.Runtime.Serialization is added as a refference and Swashbuckle.Core as a NuGet package. 
## Documentation
The documentation for this web api can be found at http://localhost:{port}/swagger/ui/index#/Messages. 
## Webservices
The available Rest Verbs are Get, Post, Put and Delete, found at http://localhost:{port}/api/messages. All the Post, Put and Delete methods need Basic authentication in order for a user to use them. Any random authentication is fine, once a user and password is written as basic authentication, another user cannot user the same for the username without the password. 



