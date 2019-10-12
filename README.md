# azure-function-extensions
Extensions for Azure Functions - Http Triggers

Use below "*HttpRequest*" Extensions to Read Posted Information to Http Trigger Functions:
- *ReadBodyAsStringAsync* - To read Posted body as string
  - Example: string body = httpRequest.ReadBodyAsStringAsync();
- *ReadJsonBodyAsync<T>* - To read posted body in json format and parse to strongly type model T
  - Example: User user = httpRequest.ReadJsonBodyAsync<User>();
- *ReadFormBodyAsync<T>* - To read posted form body and parse to strongly type model T
  - Example: User user = httpRequest.ReadFormBodyAsync<User>();
