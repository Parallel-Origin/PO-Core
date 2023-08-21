# ParallelOrigin

The core of parallel origin includes a common code base shared by the server and the client. 
This contains classes that are used on both projects to avoid duplicate code. 
A few classes still need a separate client variant because the client uses Unitys-ECS, so there are a few limitations.

# Usage

This project should be included in Client and Server as a git-submodule. 
It can not be used as a standalone project since its lacking references to the used nuget-packages. 

