# AlbumsAPI

## Description
A small API to return album/photo data. The project has been built using .Net Core 2.2.

## Requirements to run

1) .Net Core 2.2

## Projects

### AlbumsApi.Api

This project is the public facing Web API project.

### AlbumsApi.Client

This project contains the API client which calls the underlying data API.

### AlbumsApi.Common

This project contains helper methods and classes to be used by the other projects.

### AlbumsApi.Manager

This project contains the logic which combines the entities into the response format.

### AlbumsApi.UnitTests

This project contains the unit tests.

## If I had more time / Future changes

1) Implement some form of authentication / CORS support.

2) Add StyleCop or some other static code analysis tool.

