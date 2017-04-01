# LinqToMoz
Linq provider to MOZ API

The principle of library works based on [MOZ Documentation](https://moz.com/help/guides/moz-api/mozscape/overview)

Implemented metrics:

* URL Metrics
* Link Metrics

## How use it?
For use this library you should just add LINQTOMOZ namespace to your class:

```c#
using LINQTOMOZ;
```
Next step is instantiate MOZService class. Constructor of this class takes 2 arguments:

* access id
* security key

```c#
MOZService service = new MOZService("your access if","your security key");
```
