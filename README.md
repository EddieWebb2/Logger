# Net Framework Logger 4.6.2

## This is a simple Instant/Daily/Weekly scheduling assistant for .Net Framework projects.

### Current State:
- Core service and scheduling logic has been created.
- Structuremap registry added for DI.
- Scheduling logic is currently based on app.config setup.
- Serilog for service logging has been setup.

### Future State:
- Internal Add integration and unit tests to prove functionality
- Internal Api to present schedule information to consumers.
- External Api/Data connector required to modify schedule parameters.
- Internal cache required for schedule parameter refresh.
- Internal make the ProcessForever method accept generic consumers.
- External packages to be built in order to encapsulate Logging an registry dependencies.
